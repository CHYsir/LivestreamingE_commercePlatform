using RabbitMQ.Client;
using System;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

namespace LivestreamingE_commercePlatform.Helper
{
    public class RabbitMQHeleper<T>
    {
        private T numOrder;

        public int RabbitSet()
        {
            try
            {
                var factory = new ConnectionFactory();//链接工厂
                factory.HostName = "localhost";//RabbitMQ服务在本地运行
                factory.UserName = "chenjiale666";//用户名
                factory.Password = "chenjiale666";//密码
                factory.Port = 5672;
                using (var connection = factory.CreateConnection())//创建链接
                {
                    using (IModel channel = connection.CreateModel())//创建频道
                    {
                        channel.QueueDeclare(queue: "chenjiale666",//队列名称
                          durable: true, //是否持久化
                          exclusive: false,
                          autoDelete: false,
                          arguments: null);
                        channel.ExchangeDeclare(exchange: "MyChange",//路由名称
                          type: ExchangeType.Direct,//路由烈性
                          durable: true,//是否持久化
                          autoDelete: false,
                          arguments: null);
                        channel.QueueBind(queue: "hello",
                           exchange: "MyChange",
                           routingKey: string.Empty, arguments: null); //绑定路由 把hello队列和MyChange路由绑定起来 

                        var json = numOrder;
                        string jsonData = JsonConvert.SerializeObject(json);

                        byte[] body = Encoding.UTF8.GetBytes(jsonData);//消息内容转化为byte[]
                        channel.BasicPublish(exchange: "MyChange",//开始发送消息 指定路由名称
                                      routingKey: string.Empty,
                                      basicProperties: null,
                                      body: body);//发送的消息    
                        return 1;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public T RabbitGet()
        {
            var factory = new ConnectionFactory();//链接工厂
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "chenjiale666";//用户名
            factory.Password = "chenjiale666";//密码
            factory.Port = 5672;
            using (var connection = factory.CreateConnection())//创建链接
            {
                using (var channel = connection.CreateModel())//创建频道
                {



                    var consumer = new EventingBasicConsumer(channel);//在当前频道创捷接收者事件
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        numOrder = JsonConvert.DeserializeObject<T>(message);
                    };
                    channel.BasicConsume(queue: "hello",
                                    autoAck: false,              //自动回执消息
                                    consumer: consumer);
                }
            }
            return numOrder;

        }
        public string Rabbit()
        {
            try
            {
                string vs = null;
                var factory = new ConnectionFactory();//链接工厂
                factory.HostName = "localhost";//RabbitMQ服务在本地运行
                factory.UserName = "chenjiale666";//用户名
                factory.Password = "chenjiale666";//密码
                using (var connection = factory.CreateConnection())//创建链接
                {
                    using (var channel = connection.CreateModel())//创建频道
                    {

                        {
                            try
                            {
                                var consumer = new EventingBasicConsumer(channel);//在当前频道创捷接收者事件
                                consumer.Received += (model, ea) =>
                                {

                                    var body = ea.Body.ToArray();
                                    var message = Encoding.UTF8.GetString(body);
                                    vs = message;
                                };
                                channel.BasicConsume(queue: "hello",
                                                autoAck: true,              //自动回执消息
                                                consumer: consumer);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }



                    }
                }
                return vs;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}