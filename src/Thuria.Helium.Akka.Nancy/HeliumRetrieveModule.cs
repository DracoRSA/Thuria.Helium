using System;
using System.Threading.Tasks;

using Nancy;
using Nancy.Extensions;
using Nancy.IO;
using Nancy.Responses.Negotiation;

using Thuria.Zitidar.Core;

namespace Thuria.Helium.Akka.Nancy
{
  /// <summary>
  /// Helium Retrieve Nancy Module
  /// </summary>
  public class HeliumRetrieveModule : NancyModule
  {
    private readonly IThuriaSerializer _thuriaSerializer;
    private readonly IResponseNegotiator _responseNegotiator;
    private readonly IThuriaLogger _thuriaLogger;

    public HeliumRetrieveModule(IThuriaSerializer thuriaSerializer, IResponseNegotiator responseNegotiator, IThuriaLogger thuriaLogger) 
      : base("helium")
    {
      _thuriaSerializer   = thuriaSerializer ?? throw new ArgumentNullException(nameof(thuriaSerializer));
      _responseNegotiator = responseNegotiator ?? throw new ArgumentNullException(nameof(responseNegotiator));
      _thuriaLogger       = thuriaLogger ?? throw new ArgumentNullException(nameof(thuriaLogger));

      Post("/retrieve", async (parameters, token) => await ProcessRequest());
    }

    private async Task<Response> ProcessRequest()
    {
      if (Request.Body.Length <= 0)
      {
        _thuriaLogger.LogMessage(LogSeverity.Exception, "Exception: No content received in request");
        throw new BadRequestServiceErrorException("No content received in request");
      }

      try
      {
        var jsonData = ((RequestStream)this.Request.Body).AsString();
        var requestModel = this.jsonSerializer.DeserializeObject<ExperianServiceRequestDataModel>(jsonData);

        if (requestModel?.InputDataModel == null)
        {
          throw new Exception("Invalid / Missing Data Model received");
        }

        this.executionDataModel = this.CreateExecutionRecord(requestModel);
        var serviceClient = this.serviceClientRouter.FindClient(requestModel.ServiceName, requestModel.Version);

        this.sahlLogger.LogMessage(LogSeverity.Info, "Executing Experian Service using " + serviceClient);

        var (outputDataModel, resultMapping) = await serviceClient.ExecuteExperianModel(requestModel.InputDataModel);

        if (!string.IsNullOrEmpty(serviceClient.ErrorDetail))
        {
          this.sahlLogger.LogMessage(LogSeverity.Error, "Error: " + serviceClient.ErrorDetail);

          this.executionDataModel.Status = ExperianExecutionStatus.Error.ToString();
          this.executionDataModel.ExceptionInfo = serviceClient.ErrorDetail;
        }
        else
        {
          this.sahlLogger.LogMessage(LogSeverity.Info, "Success");
          this.executionDataModel.Status = ExperianExecutionStatus.Success.ToString();
        }

        this.executionDataModel.RequestCompleted = DateTime.Now;
        this.executionDataModel.OutputData = jsonSerializer.SerializeObject(outputDataModel);
        this.executionDataModel.ResultMapping = string.IsNullOrWhiteSpace(resultMapping) ? null : resultMapping;

        this.CompleteExecutionRecord();

        return new FacadeResultDataModel
        {
          ExecutionId = executionDataModel.Id,
          Status = this.executionDataModel.Status,
          ResultData = outputDataModel,
          ErrorDetail = this.executionDataModel.ExceptionInfo
        };
      }
      catch (Exception runtimeException)
      {
        this.sahlLogger.LogMessage(LogSeverity.Exception, "Exception: " + runtimeException);

        if (this.executionDataModel != null)
        {
          this.executionDataModel.Status = ExperianExecutionStatus.Exception.ToString();
          this.executionDataModel.ExceptionInfo = runtimeException.BuildExceptionErrorMessage();
          this.CompleteExecutionRecord();
        }

        throw new InternalServerErrorException("Error occurred processing Experian Service Request", runtimeException);
      }
    }
  }
}
