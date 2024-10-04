using System;

namespace Space
{
    public class Boleto
    {
        public int tarifa;
        public string linea;
        public int saldoRestante;
        public string tipoTarjeta;

        public Boleto(int tarifa1, string linea1, int saldoRestante1, string tipoTarjeta1)
        {
            this.tarifa = tarifa1;
            this.linea = linea1;
            this.saldoRestante = saldoRestante1;
            this.tipoTarjeta = tipoTarjeta1;
        }
    }
}
