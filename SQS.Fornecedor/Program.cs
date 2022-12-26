using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

var client = new AmazonSQSClient(RegionEndpoint.SAEast1);
var request = new SendMessageRequest
{
    QueueUrl = "https://sqs.sa-east-1.amazonaws.com/568119476953/teste",
    MessageBody = "Teste 123"
};

await client.SendMessageAsync(request);