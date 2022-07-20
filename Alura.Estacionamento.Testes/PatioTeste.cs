using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTeste : IDisposable
    {

        private Veiculo veiculo;
        private Patio estacionamento;
        public ITestOutputHelper SaidaConsoleTeste;

        public PatioTeste(ITestOutputHelper _saidaConsoleTeste) 
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor Invocado.");
            veiculo = new Veiculo();
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //Arrange
            var estacionamento = new Patio();
            veiculo.Proprietario = "Andre Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        [InlineData("José Silva", "POL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GDR-6524", "Azul", "Opala")]
        private void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario,
                                                        string placa,
                                                        string cor,
                                                        string modelo)
        {
            //Arange
            var estacionamento = new Patio();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNaPlaca(string proprietario,
                                            string placa,
                                            string cor,
                                            string modelo)
        {
            //Arange
            var estacionamento = new Patio();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.Placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Fact]
        public void AlterarDadosVeiculoDoProprioVeiculo() 
        {
            //Arrange
            var estacionamento = new Patio();
            veiculo.Proprietario = "Andre Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "asd-9999";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Opala";


            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo()
            {
                Proprietario = "Andre Silva",
                Tipo = TipoVeiculo.Automovel,
                Cor = "Preto", //Alterado
                Modelo = "Opala",
                Placa = "asd-9999"
            };

            //Act
            Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(veiculoAlterado.Cor, alterado.Cor);

        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Execução do Cleanup: Limpando os objetos.");
        }
    }
}
