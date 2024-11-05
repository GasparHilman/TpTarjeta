using Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Tarjeta_tests
{
    internal class Colectivo_tests
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;
        public Tarjeta tarjeta2;
        public Tarjeta medioBoleto;
        public Tarjeta gratuitoBoleto;
        public Tarjeta jubiladoboleto;
        public Colectivo expreso;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            tarjeta2 = new Tarjeta(5);
            medioBoleto = new MedioBoleto(2);
            gratuitoBoleto = new GratuitoBoleto(3);
            jubiladoboleto = new JubiladoBoleto(4);
            k = new Colectivo("K");
            expreso = new ColectivoInterurbano("expreso");
        }

        [Test]
        public void PagarConSaldoPositivo()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2 * k.precio));
        }

        [Test]
        public void PagarSINSaldo()
        {
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(0));

        }
        [Test]
        public void PagarConSaldoNegativo()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2 * k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2 * k.precio));
        }

        [Test]
        public void MenosDe5Min_MedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(2000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(1);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(2000 - (k.precio / 2) - k.precio));
        }
        [Test]
        public void cuatro_boletos_Medios()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - ((k.precio / 2) * 4)));
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - ((k.precio / 2) * 4) - k.precio));
        }
        [Test]
        public void dos_boletos_Gratuitos()
        {
            tiempo.AgregarHoras(7);
            gratuitoBoleto.Cargar_tarjeta(9000);
            k.PagarCon(gratuitoBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            k.PagarCon(gratuitoBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000));
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void MasDe5Min_MedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(7);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - 2 * (k.precio / 2)));
        }
        [Test]
        public void PagoconCredito()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(3000);

            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
            Assert.That(tarjeta.credito, Is.EqualTo(39000 - tarjeta.saldo_max));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
        }

        [Test]
        public void PagosinCredito()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);
            tarjeta.Cargar_tarjeta(9000);

            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max));
            Assert.That(tarjeta.credito, Is.EqualTo(0));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(tarjeta.saldo_max - k.precio));
        }

        [Test]
        public void PrimerUsoFrecuente_Boleto29()
        {

            tarjeta.setear_viajesmes(28);
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 29
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(29));
        }

        [Test]
        public void SegundoUsoFrecuente_Boleto30()
        {
            tarjeta.setear_viajesmes(29);
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 30
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - ((int)(k.precio * 0.8))));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(30));
        }

        [Test]
        public void DescuentoEnViaje80()
        {
            tarjeta.setear_viajesmes(79);
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 80
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - ((int)(k.precio * 0.75))));
        }

        [Test]
        public void SinDescuentoEnViaje81()
        {
            tarjeta.setear_viajesmes(80);
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 81
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void MedioBoletoALas0000()
        {
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void UsoGratuitoBoletoALas0000()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }
        [Test]
        public void MedioBoletoALas7()
        {
            medioBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);

            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - k.precio / 2));
        }

        [Test]
        public void JubiladoBoletoALas7()
        {
            jubiladoboleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);

            k.PagarCon(jubiladoboleto, tiempo);
            Assert.That(jubiladoboleto.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void Pago_jubiladoBoleto_dia_Habil()
        {
            jubiladoboleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(9);

            k.PagarCon(jubiladoboleto, tiempo);
            Assert.That(jubiladoboleto.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void Pago_jubiladoBoleto_dia_NO_Habil_horaHabil()
        {
            jubiladoboleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);
            tiempo.AgregarDias(6); //domingo 07:00

            k.PagarCon(jubiladoboleto, tiempo);
            Assert.That(jubiladoboleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void Pago_jubiladoBoleto_dia_NO_Habil_hora_NO_Habil()
        {
            jubiladoboleto.Cargar_tarjeta(9000);
            tiempo.AgregarDias(6); //domingo 00:00

            k.PagarCon(jubiladoboleto, tiempo);
            Assert.That(jubiladoboleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void Pago_jubiladoBoleto_siempre()
        {
            jubiladoboleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);
            for (int i = 0; i < 20; i++) {
                k.PagarCon(jubiladoboleto, tiempo);
                Assert.That(jubiladoboleto.saldo, Is.EqualTo(9000));
            }
        }


        [Test]
        public void GratuitoBoletoALas7()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);

            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void Pago_GratuitoBoleto_dia_Habil()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(9);

            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void Pago_GratuitoBoleto_dia_NO_Habil_horaHabil()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);
            tiempo.AgregarDias(6); //domingo 07:00

            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void Pago_GratuitoBoleto_dia_NO_Habil_hora_NO_Habil()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarDias(6); //domingo 00:00

            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void Pago_medioBoleto_dia_Habil()
        {
            medioBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(9);

            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - (k.precio / 2)));
        }

        [Test]
        public void Pago_medioBoleto_dia_NO_Habil_horaHabil()
        {
            medioBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);
            tiempo.AgregarDias(6); //domingo 07:00

            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void Pago_medioBoleto_dia_NO_Habil_hora_NO_Habil()
        {
            medioBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarDias(6); //domingo 00:00

            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void SaldoInterurbano()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta2.Cargar_tarjeta(9000);

            expreso.PagarCon(tarjeta, tiempo);
            k.PagarCon(tarjeta2, tiempo);

            Assert.That(tarjeta.saldo, Is.LessThan(tarjeta2.saldo));
        }


    }
}

