using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

//~~> Posso ter vários fonecedores para uma fila.
//~~> Posso ter somente um consumidor ou várias instância do mesmo tipo para consumir a fila.
//~~>é possível configurar o tempo do item para retornar para fila.

var client = new AmazonSQSClient(RegionEndpoint.SAEast1);
var request = new ReceiveMessageRequest
{
    QueueUrl = "https://sqs.sa-east-1.amazonaws.com/568119476953/teste"
};


while (true)
{
    var response = await client.ReceiveMessageAsync(request);

    foreach (var message in response.Messages)
    {
        Console.WriteLine(message?.Body);

        //Deletando messagem da fila senão irá ser processado por outro.
        await client.DeleteMessageAsync("https://sqs.sa-east-1.amazonaws.com/568119476953/teste", message.ReceiptHandle);
    }
}

