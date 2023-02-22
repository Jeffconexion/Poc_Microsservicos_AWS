using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Model;
using Newtonsoft.Json;

namespace Compartilhado
{
    public static class AmazonUtil
    {
        public static async Task SalvarAsync(this Pedido pedido)
        {
            var client = new AmazonDynamoDBClient(RegionEndpoint.SAEast1);
            var context = new DynamoDBContext(client);

            await context.SaveAsync(pedido);
        }

        public static T ToObject<T>(this Dictionary<string, AttributeValue> dictionary)
        {
            var client = new AmazonDynamoDBClient(RegionEndpoint.SAEast1);
            var context = new DynamoDBContext(client);

            var doc = Document.FromAttributeMap(dictionary);
            return context.FromDocument<T>(doc);
        }

        public static async Task EnviarParaFila(EnumFilaSQS fila, Pedido pedido)
        {
            var json = JsonConvert.SerializeObject(pedido);
            var client = new AmazonSQSClient(RegionEndpoint.SAEast1);
            var request = new SendMessageRequest
            {
                QueueUrl = $"https://sqs.sa-east-1.amazonaws.com/406390286801/{fila}",
                MessageBody = json
            };
            await client.SendMessageAsync(request);
        }

        public static async Task EnviarParaFila(EnumFilaSns fila, Pedido pedido)
        {
            await Task.CompletedTask;
        }
    }
}
