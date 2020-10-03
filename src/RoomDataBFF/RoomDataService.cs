using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

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

            var request = new QueryRequest
            {
                TableName = "RoomData",
                KeyConditionExpression = "Id = :v_Id",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
        {":v_Id", new AttributeValue { S =  "Amazon DynamoDB#DynamoDB Thread 1" }}}
            };

            var response = await client.QueryAsync(request);

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                // Process the result.
            System.Console.WriteLine(item);
            }

            return new List<string>
            {
                "value1",
                "value2"
            };
        }
    }
}