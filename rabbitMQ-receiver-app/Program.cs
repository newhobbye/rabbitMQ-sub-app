using rabbitMQ_receiver_app.Services;

string message = await MessageService.ReceiveMessageAsync();
Console.WriteLine(message);
