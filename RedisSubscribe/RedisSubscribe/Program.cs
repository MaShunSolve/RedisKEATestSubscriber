using System;
using StackExchange.Redis;

namespace RedisSubscribe
{
    public class Program
    {
        public static string RedisConnectStr = "XXX.XX.XX.XX:XXXX,abortConnect=false,password=****";//Redis連線字串 替換IP與password
        public static int changeCount = 0;
        //訂閱者
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnectStr);
            //IDatabase db = redis.GetDatabase(1);
            var redisSubscriber = redis.GetSubscriber();
            redisSubscriber.Subscribe("__keyspace@0__:Monkey", (channel, message) =>
            {
                changeCount++;
                Console.WriteLine($"{DateTime.Now}收到{channel}訊息 : {message} 累積第{changeCount}筆通知");
            });
            Console.WriteLine("已訂閱 messages");
            Console.ReadKey();
        }

    }
}

