
using NUnit.Framework;
using Space;


namespace TP_Tarjeta_tests
{
    internal class Iteracion4_tests
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;
        public Tarjeta tarjeta2;
        public Tarjeta medioBoleto;
        public Tarjeta gratuitoBoleto;
        public Colectivo expreso;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            tarjeta2 = new Tarjeta(5);
            medioBoleto = new MedioBoleto(2);
            gratuitoBoleto = new GratuitoBoleto(3);
            k = new Colectivo("K");
            expreso = new ColectivoInterurbano("expreso");
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