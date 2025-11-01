// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//Console.ReadKey();
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Practica1_ClienteServidor_Sockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Aplicación Servidor ***");
            while (true)
            {
                try
                {
                    IPAddress ipAd = IPAddress.Parse("25.0.248."); // IP del servidor
                    int port = 6515; // Usamos el puerto 6515 según tu configuración
                    TcpListener myList = new TcpListener(ipAd, port);

                    // Habilitar reutilización de la dirección
                    myList.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    myList.Start();
                    Console.WriteLine("Servidor iniciado en el puerto " + port + "...");
                    Console.WriteLine("Esperando conexión ...");

                    Socket s = myList.AcceptSocket();
                    Console.WriteLine("Conexión recibida desde: " + s.RemoteEndPoint);

                    byte[] b = new byte[2048]; // Aumento s 2048 bytes
                    int k = s.Receive(b);
                    string cadena = Encoding.ASCII.GetString(b, 0, k);
                    Console.WriteLine("Cadena recibida: " + cadena);

                    string conectSQL = "Data Source=CIANRA;Initial Catalog=Nueva_BDD_Practica1; User Id=cinthia;Password=Astro123; TrustServerCertificate=True;";
                    using (SqlConnection cm = new SqlConnection(conectSQL))
                    {
                        cm.Open();
                        SqlCommand cmd = new SqlCommand(cadena, cm);
                        cmd.ExecuteNonQuery();
                    }

                    byte[] response = Encoding.ASCII.GetBytes("Cadena recibida. Comando ejecutado");
                    s.Send(response);
                    Console.WriteLine("Confirmación enviada");

                    s.Close();
                    myList.Stop();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError ... " + e.StackTrace + " ---" + e.Message);
                }
            }
        }
    }
}