using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RoomDataBFF.Models;

namespace RoomDataBFF
{
    public interface IRoomDataService
    {
        Task<IEnumerable<string>> GetValues();
    }

    public class RoomDataService : IRoomDataService
    {
        public async Task<IEnumerable<string>> GetValues()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            DynamoDBContext context = new DynamoDBContext(client);

            FindRepliesInLast15Days(context, "StudyNorth");

            // var request = new QueryRequest
            // {
            //     TableName = "RoomData",
            //     KeyConditionExpression = "roomid = :v_Id and datetimeunix BETWEEN :t1 AND :t2",
            //     ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
            //     {":v_Id", new AttributeValue { S =  "StudyNorth" }},
            //      {":t1", new AttributeValue { N =  "1600171921.772" }},
            //       {":t2", new AttributeValue { N =  "1600172161.303" }
            //     }}
            // };

            // var response = await client.QueryAsync(request);
            // System.Console.WriteLine(response.Items.Count);
            //Console.WriteLine("\nNo. of reads used (by query in FindRepliesPostedWithinTimePeriod) {0}",
            //            response.ConsumedCapacity.CapacityUnits);
            // foreach (Dictionary<string, AttributeValue> item in response.Items)
            // {
            //     // Process the result.
            //     foreach (var keyValuePair in item)
            //     {
            //         var lines = item.Select(x=>x.Key + ":" + x.Value);
            //         System.Console.WriteLine(String.Join(Environment.NewLine,lines));
            //     }
            // }
            return new List<string>
            {
                "value1",
                "value2"
            };

        }
        private async Task FindRepliesInLast15Days(DynamoDBContext context,
                                string forumName)
        {
            DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
            // List<string> lists = new List<string>() { 1600171921.772, 1600172161.303 };
            try
            {
                System.Console.WriteLine("\nFindRepliesInLast15Days: Printing result.....");
                var latestReplies =
                       await context.QueryAsync<RoomData>(forumName,
                        QueryOperator.Between,
                         new object[] { 1600170541.25, 1601170541.25 }
                         ).GetRemainingAsync();
                System.Console.WriteLine("\nFindRepliesInLast15Days: Printing result.....");
                foreach (RoomData r in latestReplies)
                    System.Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                    r.RoomId,
                    r.DateTimeUnix,
                    r.AirQualityPercent,
                    r.HumidityPercent,
                    r.GasResistanceOhms,
                    r.HumidityPercent,
                    r.PressureHpa,
                    r.TemperatureCelsius);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine(e.StackTrace);

            }



        }
    }
}