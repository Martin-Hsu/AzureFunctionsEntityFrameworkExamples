using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace chtline_azure_function
{
    public static class SendLine
    {
        [FunctionName("SendLine")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sendline")]HttpRequestMessage req, TraceWriter log)
        {
            
            string ChannelAccessToken = "Xffljh8lPenUaABqyV1o7dLMhnAiPxe6uAM393tjF+jKKgnf5XHnd4lC+r6D36l7NcyMwRPPyMPL2Br2HUdbtwMKYWx15VQKmZB45JSNOJKJlIATO5xmw3spd1GLfGn3PyehB5De9sqEyfN/UeYWAwdB04t89/1O/w1cDnyilFU=";
            
            string postData = req.Content.ReadAsStringAsync().Result;
            log.Info("api SendLine:"+postData);
            try
            {
                var k = (LineInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(postData, typeof(LineInfo));
                log.Info("SendLine:" + postData);
                log.Info("SendLine:" + k.UserID);
                log.Info("SendLine:" + k.Message);
                string[] userids = k.UserID.Split(';');


                foreach (string userid in userids)
                {
                    isRock.LineBot.Utility.PushMessage(userid, k.Message, ChannelAccessToken);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return req.CreateResponse(HttpStatusCode.OK);
        }


        /// <summary>
        /// 舊的專案 使用 LineInfo
        /// </summary>
        public class LineInfo
        {
            public string UserID { set; get; }
            public string Message { set; get; }
            public LineInfo(string _userid, string _message)
            {
                UserID = _userid;
                Message = _message;
            }
        }

        public class LineInfoWithPic
        {
            public string UserID { set; get; }
            public string Message { set; get; }

            public string Account { set; get; }

            public string UpLayerName { set; get; }

            public int LogID { set; get; }

            public string tag { set; get; }

            public string AccessImagePath { set; get; }

            public string ChannelAccessToken { set; get; }

            public LineInfoWithPic(string _userid, string _message)
            {
                UserID = _userid;
                Message = _message;
            }

            public string toString()
            {
                string result = "";

                return result;
            }
        }
    }
}
