using Desafio.Api.Models;
namespace Desafio.Tests;

public class TestsCpf
{
    [Fact]
    public void FormatCPF_UnformattedCPF_ReturnsFormattedCPF()
    {
        // arrange
        string cpf = "12345678901";

        // act
        string formattedCpf = CPFs.FormatCPF(cpf);

        // assert
        Assert.Equal("123.456.789-01", formattedCpf);
    }

    [Fact]
    public void RemoveDigitsCPF_FormattedCPF_ReturnsCpfWithoutDigits()
    {
        string cpf = "123.456.789-01";

        string unformattedCpf = CPFs.RemoveDigitsCPF(cpf);

        Assert.Equal("12345678901", unformattedCpf);
    }

    [Fact]
    public void ValidCpf_InvalidCPF_ReturnFalse()
    {
        string cpf = "123.456.789-01";
        cpf = CPFs.RemoveDigitsCPF(cpf);

        bool validation = CPFs.ValidCPF(cpf);

        Assert.False(validation);
    }

    [Fact]
    public void ValidCPF_ValidCPF_ReturnTrue()
    {
        string cpf = "775.010.260-41";
        cpf = CPFs.RemoveDigitsCPF(cpf);

        bool validation = CPFs.ValidCPF(cpf);

        Assert.True(validation);
    }
}