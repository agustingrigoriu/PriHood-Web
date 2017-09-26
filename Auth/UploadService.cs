using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using System;

namespace PriHood.Auth
{
  public class UploadService
  {
    public Object UploadFiles(IFormFile file)
    {
      try
      {
        var fileTransferUtility = new TransferUtility(new AmazonS3Client("AKIAJIMIZBFD7RT4N7BA", "Z1FxzXeynKgOg1GkPmre0JI4xvzVoR1Ew2/PiDkP", Amazon.RegionEndpoint.USWest2));
        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
          BucketName = "prihood",
          InputStream = file.OpenReadStream(),
          Key = file.FileName,
          CannedACL = S3CannedACL.PublicRead
        };
        
        fileTransferUtility.Upload(fileTransferUtilityRequest);

        return new { error = false, data = "ok" };
      }
      catch (Exception e)
      {
        return new { error = true, data = e.Message, e.StackTrace };
      }
    }
  }
}
