using System;
using System.Collections.Generic;
using System.Linq;


namespace Space
{
    public class Colectivo
    {
        private int tarifa;
        public int precio { get; protected set; }
        public string linea;
        private string TipoTarjeta;
        private bool flag_viajeshoy = false;

        public Colectivo(string linea1)
        {
            linea = linea1;
            precio = 1200;
        }

        public bool Descontar(Tarjeta tarjeta, Tiempo tiempo)
        {
            if (tarjeta is JubiladoBoleto) {
                tarifa = precio;
                if (tiempo.Now().Hour >= 6 && tiempo.Now().Hour <= 22 && tiempo.Now().DayOfWeek != DayOfWeek.Sunday && tiempo.Now().DayOfWeek != DayOfWeek.Saturday)
                {
                    tarifa = 0;
                    TipoTarjeta = "Boleto Jubilado";
                }

            } else { 
            if (tarjeta is GratuitoBoleto)
            {
                tarifa = precio;
                flag_viajeshoy = false;
                if (tiempo.Now().Hour >= 6 && tiempo.Now().Hour <= 22 && tiempo.Now().DayOfWeek != DayOfWeek.Sunday && tiempo.Now().DayOfWeek != DayOfWeek.Saturday)
                { flag_viajeshoy = true;
                    if (tarjeta.historial.Count != 0)
                    {
                        if (tarjeta.historial.LastOrDefault().UltimoViaje.DayOfYear != tiempo.Now().DayOfYear)
                        {
                            tarjeta.reiniciar_viajeshoy();
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
                if (tarjeta is MedioBoleto)
                {
                    tarifa = precio;
                    flag_viajeshoy = false;
                    if (tiempo.Now().Hour >= 6 && tiempo.Now().Hour <= 22 && tiempo.Now().DayOfWeek != DayOfWeek.Sunday && tiempo.Now().DayOfWeek != DayOfWeek.Saturday)
                    {
                        flag_viajeshoy = true;
                        if (tarjeta.historial.Count != 0)
                        {
                            if (tarjeta.historial.LastOrDefault().UltimoViaje.DayOfYear != tiempo.Now().DayOfYear)
                            {

                                tarjeta.reiniciar_viajeshoy();
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
                    TipoTarjeta = "Medio Boleto";
                }
                else
                {

                    if (tarjeta.historial.Count != 0)
                    {
                        if (tarjeta.historial.LastOrDefault().UltimoViaje.Month != tiempo.Now().Month || tarjeta.historial.LastOrDefault().UltimoViaje.Year != tiempo.Now().Year)
                        {
                            tarjeta.setear_viajesmes(0);

                        }
                    }
                    tarifa = precio;
                    if (tarjeta.viajesmes >= 29 && tarjeta.viajesmes <= 78)
                    {
                        tarifa = (int)(precio * 0.8);
                    }
                    if (tarjeta.viajesmes == 79)
                    {
                        tarifa = (int)(precio * 0.75);
                    }


                    TipoTarjeta = "Boleto Normal";
                }
            }
            }
            if (tarjeta.saldo - tarifa >= tarjeta.limite_neg)
            {

                if (flag_viajeshoy)
                {
                    tarjeta.aumentar_viajeshoy();
                }

                if (tarjeta.credito == 0)
                {
                    tarjeta.restar_saldo(tarifa);
                }
                else
                {

                    if (tarjeta.credito >= tarifa)
                    {
                        tarjeta.restar_credito(tarifa);

                    }
                    else
                    {
                        tarjeta.restar_saldo(tarifa - tarjeta.credito);
                        tarjeta.reiniciar_credito();
                       
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
