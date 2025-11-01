// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//Console.ReadKey();
using System;
using System.Net.Sockets;
using System.Text;

namespace Practica1_ClienteServidor_Sockets_Cliente
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("** Aplicación Cliente **");
            while (true)
            {
                try
                {
                    TcpClient tcpClnt = new TcpClient();
                    Console.WriteLine("Conectando ...");
                    // utilizamos la IP del servidor 
                    tcpClnt.Connect("25.0.248.40", 6515);
                    Console.WriteLine("Conectado al servidor");
                    Console.WriteLine("Introduzca la cadena a ejecutar: ");
                    String str = Console.ReadLine();
                    Stream stm = tcpClnt.GetStream();
                    // Convertimos la cadena a ASCII para transmitirla
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Transmitiendo la cadena ...");
                    stm.Write(ba, 0, ba.Length);
                    // Recibir confirmación, se debe convertir a string
                    byte[] bb = new byte[100];
                    int k = stm.Read(bb, 0, 100);
                    string acuse = "";
                    for (int i = 0; i < k; i++)
                        acuse = acuse + Convert.ToChar(bb[i]);
                    Console.WriteLine(acuse);
                    tcpClnt.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error ... " + e.StackTrace);
                }
            }
        }
    }
}

