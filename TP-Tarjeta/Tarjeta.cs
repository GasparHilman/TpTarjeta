using System;
using System.Collections.Generic;

namespace Space
{
    public class Tarjeta
    {

        public int saldo { get; private set; }
        private int id;
        public int saldo_max{ get; private set; }
        public int limite_neg  { get; private set; }
        private int[] montos_posibles = new int[] { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        public List<Boleto> historial = new List<Boleto>();
        public int viajesHoy { get; private set; }
        public int viajesmes { get; private set; }
        public int credito { get; private set; }

        public void mostrarsaldo()
        {
            Console.WriteLine("Saldo: " + saldo);
        }

        public Tarjeta(int id)
        {
            viajesHoy = 0;
            this.id = id;
            saldo = 0;
            credito = 0;
            saldo_max = 36000;
            limite_neg = -480;
        }

        public void Cargar_tarjeta(int monto)
        {

            bool monto_valido = false;
            foreach (int monto_posible in montos_posibles)
            {
                if (monto_posible == monto)
                {
                    monto_valido = true;
                    break;
                }
            }

            if (!monto_valido)
            {
                Console.WriteLine("El monto a cargar no es posible");
            }
            else
            {

                if (monto + saldo > saldo_max)
                {
                    credito = (monto + saldo) - saldo_max;
                    saldo = saldo_max;

                }
                else
                {

                    saldo = monto + saldo;

                }
            }
        }
    }
}
