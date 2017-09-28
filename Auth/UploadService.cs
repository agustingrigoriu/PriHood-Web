using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using System;

namespace PriHood.Auth
{
  public class UploadService
  {
    public string UploadFile(IFormFile file)
    {
      var extension = file.FileName.Split('.');
      var filename = Guid.NewGuid() + @"." + extension[extension.Length - 1];
      var fileTransferUtility = new TransferUtility(new AmazonS3Client("AKIAJIMIZBFD7RT4N7BA", "Z1FxzXeynKgOg1GkPmre0JI4xvzVoR1Ew2/PiDkP", Amazon.RegionEndpoint.USWest2));
      var fileTransferUtilityRequest = new TransferUtilityUploadRequest
      {
        BucketName = "prihood",
        InputStream = file.OpenReadStream(),
        Key = filename,
        CannedACL = S3CannedACL.PublicRead
      };

      fileTransferUtility.Upload(fileTransferUtilityRequest);

      return "https://s3-us-west-2.amazonaws.com/prihood/" + filename;
    }
  }
}
