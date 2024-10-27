using System;
using System.Collections.Generic;
using System.Linq;


namespace Space
{
    public class Colectivo
    {
        public int tarifa;
        public int precio = 1200;
        public string linea;
        public string TipoTarjeta;

        public Colectivo(string linea1)
        {
            this.linea = linea1;
        }

        public bool Descontar(Tarjeta tarjeta, Tiempo tiempo)
        {
            if (tarjeta is GratuitoBoleto)
            {
                tarifa = precio;
                if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 22)
                {
                    if (tarjeta.historial.Count != 0)
                    {
                        if (tarjeta.historial.LastOrDefault().UltimoViaje.Day != tiempo.Now().Day)
                        {
                            tarjeta.viajesHoy = 0;
                        }
                    }
                    if (tarjeta.viajesHoy < 2)
                    {

                        tarifa = 0;

                    }
                    else
                    {

                        tarifa = precio;
                    }

                    TipoTarjeta = "Boleto Gratuito"; 

                }
            }
            else
            {
                tarifa = precio;
                if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 22)
                {
                    if (tarjeta is MedioBoleto)
                    {
                        if (tarjeta.historial.Count != 0)
                        {
                            if (tarjeta.historial.LastOrDefault().UltimoViaje.Day != tiempo.Now().Day)
                            {

                                tarjeta.viajesHoy = 0;
                                tarifa = precio / 2;
                            }
                        }

                        if (tarjeta.viajesHoy < 4 && tarjeta.viajesHoy > 0)
                        {

                            if (tarjeta.historial.LastOrDefault().UltimoViaje.Hour == tiempo.Now().Hour ? (tiempo.Now().Minute - tarjeta.historial.LastOrDefault().UltimoViaje.Minute) > 5 : true)
                            {
                                tarifa = precio / 2;

                            }
                            else
                            {

                                tarifa = precio;
                            }

                        }
                        else
                        {

                            tarifa = precio;
                        }
                        if (tarjeta.historial.Count == 0)
                        {
                            tarifa = precio / 2;
                        }

                        TipoTarjeta = "Medio Boleto";
                    }
                }
                else
                {      
                    if(tarjeta.historial.Last().UltimoViaje.Month != DateTime.Now.Month || tarjeta.historial.Last().UltimoViaje.Year != DateTime.Now.Year)
                    {
                        tarjeta.viajesmes = 0;

                    }
                    tarifa = precio;
                    if (tarjeta.viajesmes >= 30 && tarjeta.viajesmes <= 79)
                    {
                        tarifa = (int)(precio * 0.8);
                    }
                    if (tarjeta.viajesmes ==80)
                    {
                        tarifa = (int)(precio * 0.75);
                    }


                    TipoTarjeta = "Boleto Normal";
                }
            }

            if (tarjeta.saldo - tarifa >= tarjeta.limite_neg)
            {
                if (tarjeta.credito == 0)
                {

                    tarjeta.saldo -= tarifa;
                    tarjeta.viajesHoy++;

                }
                else
                {

                    if (tarjeta.credito >= tarifa)
                    {
                        tarjeta.credito -= tarifa;

                    }
                    else
                    {
                        tarjeta.saldo -= tarifa - tarjeta.credito;
                        tarjeta.credito = 0;
                    }
                }

                return true;
            }
            else
            {
                Console.WriteLine("No se pudo pagar. Saldo insuficiente.");
                return false;
            }
        }

        public Boleto PagarCon(Tarjeta tarjeta, Tiempo tiempo)
        {
            if (Descontar(tarjeta, tiempo))
            {
                tarjeta.historial.Add(new Boleto(tarifa, linea, tarjeta.saldo, TipoTarjeta, tarjeta.id, tiempo));
                tarjeta.viajesmes += 1;
                return new Boleto(tarifa, linea, tarjeta.saldo, TipoTarjeta, tarjeta.id, tiempo);
            }
            else
            {
                Console.WriteLine("No se pudo emitir el boleto.");
                return null;
            }
        }
    }
}
