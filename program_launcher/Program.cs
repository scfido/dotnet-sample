using System.Net.WebSockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        // 使用当前进程的文件名作为参数，调用TryGetStartInfo
        

        Console.WriteLine("Hello, World!");

    }

    // 有一个文件名，格式是“程序名_模块名.exe”，例如“notepad_kernel32.dll.exe”，请写一个函数，返回程序名和模块名。
    bool TryGetStartInfo(string filename, out string program, out string mainModule)
    {
        program = null;
        mainModule = null;
        if (string.IsNullOrEmpty(filename))
        {
            return false;
        }

        var index = filename.LastIndexOf('_');
        if (index < 0)
        {
            return false;
        }

        program = filename.Substring(0, index);
        mainModule = filename.Substring(index + 1);

        if (string.IsNullOrEmpty(program) || string.IsNullOrEmpty(mainModule))
        {
            return false;
        }

        return true;
    }

    //使用websocket协议链接到服务器
    private static async Task Connect()
    {
        var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri("ws://localhost:8080/"), CancellationToken.None);
        Console.WriteLine("连接成功");

        var buffer = new byte[1024];
        while (true)
        {
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("连接已关闭");
                break;
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine("收到消息：" + message);
            }
        }
    }

}