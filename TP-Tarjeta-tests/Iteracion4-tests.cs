using NUnit.Framework;
using Space;


namespace TP_Tarjeta_tests
{
    public class Test_siteracion4
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

        public void ChequeoIngresosSaldosPosibles()
        {
            tarjeta.Cargar_tarjeta(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));

            tarjeta.Cargar_tarjeta(3000);
            Assert.That(tarjeta.saldo, Is.EqualTo(3000));

            tarjeta.Cargar_tarjeta(4000);
            Assert.That(tarjeta.saldo, Is.EqualTo(4000));

            tarjeta.Cargar_tarjeta(5000);
            Assert.That(tarjeta.saldo, Is.EqualTo(5000));

            tarjeta.Cargar_tarjeta(6000);
            Assert.That(tarjeta.saldo, Is.EqualTo(6000));

            tarjeta.Cargar_tarjeta(7000);
            Assert.That(tarjeta.saldo, Is.EqualTo(7000));

            tarjeta.Cargar_tarjeta(8000);
            Assert.That(tarjeta.saldo, Is.EqualTo(8000));

            tarjeta.Cargar_tarjeta(9000);
            Assert.That(tarjeta.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void CargarSaldoNoValido_TarjetaNormal()
        {
            tarjeta.Cargar_tarjeta(2000); 
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));

            tarjeta.Cargar_tarjeta(0); 
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));

            tarjeta.Cargar_tarjeta(-500); 
            Assert.That(tarjeta.saldo, Is.EqualTo(2000)); 
        }
        [Test]
        public void PagarConSaldoPositivo()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta,tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2*k.precio));
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
        public void mostrarBoleto_TarjetaNormal()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta, tiempo);
            tarjeta.historial.Last().mostrarboleto();
        }

        [Test]
        public void mostrarBoleto_TarjetaMedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(2000);
            k.PagarCon(medioBoleto, tiempo);
            medioBoleto.historial.Last().mostrarboleto();
        }

        [Test]
        public void mostrarBoleto_TarjetaGratuitoBoleto()
        {
            tiempo.AgregarHoras(7);
            gratuitoBoleto.Cargar_tarjeta(2000);
            k.PagarCon(gratuitoBoleto, tiempo);
            gratuitoBoleto.historial.Last().mostrarboleto();
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
        public void MasDe5Min_MedioBoleto()
        {
            tiempo.AgregarHoras(7);
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            tiempo.AgregarMinutos(7);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - 2*(k.precio/2)));
        }

        [Test]
        public void PrimerUsoFrecuente_Boleto29()
        {
            tarjeta.viajesmes = 28;
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 29
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(29));
        }

        [Test]
        public void SegundoUsoFrecuente_Boleto30()
        {
            tarjeta.viajesmes = 29;
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 30
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - ((int)(k.precio * 0.8))));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(30));
        }

        [Test]
        public void DescuentoEnViaje80()
        {
            tarjeta.viajesmes = 79;
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 80
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - ((int)(k.precio * 0.75))));
        }

        [Test]
        public void SinDescuentoEnViaje81()
        {
            tarjeta.viajesmes = 80;
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta, tiempo); // boleto 81
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio ));
        }

        [Test]
        public void FranjaHorariaUsoMedioBoletoALas0000()
        {
            medioBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }

        [Test]
        public void FranjaHorariaUsoGratuitoBoletoALas0000()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000 - k.precio));
        }
        [Test]
        public void FranjaHoraria_DescuentoMedioBoletoALas7()
        {
            medioBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);

            k.PagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000 - k.precio / 2));
        }

        [Test]
        public void FranjaHoraria_DescuentoGratuitoBoletoALas7()
        {
            gratuitoBoleto.Cargar_tarjeta(9000);
            tiempo.AgregarHoras(7);

            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void ComparacionSaldoEntreInterurbanoYUrbano()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta2.Cargar_tarjeta(9000);

            expreso.PagarCon(tarjeta, tiempo);
            k.PagarCon(tarjeta2, tiempo);

            Assert.That(tarjeta.saldo, Is.LessThan(tarjeta2.saldo));
        }

    }
}