using System;
using System.Collections.Generic;
using TP_Tarjeta;

namespace Space
{
    public class Boleto
    {
        public int tarifa;
        public string linea;
        public int saldoRestante;
        public string tipoTarjeta;
        public DateTime UltimoViaje;
        public int idTarjeta;
        

        public Boleto(int tarifa1, string linea1, int saldoRestante1, string tipoTarjeta1, int idTarjeta1, Tiempo tiempo)
        {
            this.tarifa = tarifa1;
            this.linea = linea1;
            this.saldoRestante = saldoRestante1;
            this.tipoTarjeta = tipoTarjeta1;
            this.UltimoViaje = tiempo.Now();
            this.idTarjeta = idTarjeta1;

        }

        public void mostrarboleto()
        {
            Console.WriteLine("Tarifa: " + tarifa);
            Console.WriteLine("Linea: " + linea);
            Console.WriteLine("Fecha: " + UltimoViaje);
            Console.WriteLine("Saldo Restante: " + saldoRestante);
            Console.WriteLine("Tipo de Tarjeta: " + tipoTarjeta);
            Console.WriteLine("Id de la Tarjeta: " + idTarjeta);
        }
    }
}
