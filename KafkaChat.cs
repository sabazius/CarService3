using Confluent.Kafka;
using KafkaChat.Serialization;
using MessagePack;

namespace KafkaChat
{
    // Модел на съобщението
    [MessagePackObject]
    public class ChatMessage
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public string Sender { get; set; }

        [Key(2)]
        public string Text { get; set; }

        [Key(3)]
        public DateTime Timestamp { get; set; }

        public ChatMessage() { }

        public ChatMessage(string id, string sender, string text, DateTime timestamp)
        {
            Id = id;
            Sender = sender;
            Text = text;
            Timestamp = timestamp;
        }
    }

    class Program
    {
        private static string _nickname = string.Empty;
        private const string Topic = "pu-chat";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Въведи своя nickname: ");
            _nickname = Console.ReadLine() ?? "Анонимен";

            Console.Clear();
            Console.WriteLine($"--- Добре дошъл, {_nickname}! (Използвайте Ctrl+C за изход) ---");

            // Стартираме четенето в бекграунд
            var cts = new CancellationTokenSource();
            var consumerTask = Task.Run(() => StartConsumer(cts.Token));

            // Основен цикъл за изпращане
            await StartProducer();

            cts.Cancel();
            await consumerTask;
        }

        // --- PRODUCER (Изпращане) ---
        static async Task StartProducer()
        {
            var config = new ProducerConfig 
            {
                BootstrapServers = "kafka-210718-0.cloudclusters.net:10020",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "puchat",
                SaslPassword = "1234567q",
                EnableSslCertificateVerification = false
            };

            using var producer = new ProducerBuilder<string, byte[]>(config).Build();

            while (true)
            {
                string input = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(input)) continue;

                var msg = new ChatMessage(Guid.NewGuid().ToString(),_nickname, input, DateTime.Now);
                byte[] serializedMsg = MessagePackSerializer.Serialize(msg);

                await producer.ProduceAsync(Topic, new Message<string, byte[]> { Value = serializedMsg });
            }
        }

        // --- CONSUMER (Приемане) ---
        static void StartConsumer(CancellationToken token)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka-210718-0.cloudclusters.net:10020",
                GroupId = $"KafkaChat{Guid.NewGuid}",
                AutoOffsetReset = AutoOffsetReset.Latest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "puchat",
                SaslPassword = "1234567q",
                EnableSslCertificateVerification = false
            };

            using var consumer = new ConsumerBuilder<string, byte[]>(config).Build();
            consumer.Subscribe(Topic);

            try
            {
                while (!token.IsCancellationRequested)
                {
                    var result = consumer.Consume(token);
                    var msg = MessagePackSerializer.Deserialize<ChatMessage>(result.Message.Value);

                    DisplayMessage(msg);
                }
            }
            catch (OperationCanceledException) { }
            finally { consumer.Close(); }
        }

        // --- ИНТЕРФЕЙС (mIrc стил) ---
        static void DisplayMessage(ChatMessage msg)
        {
            string time = msg.Timestamp.ToString("HH:mm");

            if (msg.Sender == _nickname)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{time}> <{msg.Sender}> {msg.Text}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"<{time}> <{msg.Sender}> {msg.Text}");
            }

            Console.ResetColor();
        }
    }
}
