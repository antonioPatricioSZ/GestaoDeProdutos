using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Cryptography;

public class PasswordEncryptor {

    public static string Criptografar(string senha) {
        var bytes = Encoding.UTF8.GetBytes(senha);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes) {
        var sb = new StringBuilder();
        foreach (byte b in bytes) {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}


    // O código que você forneceu é uma implementação em C# de uma função que
    // calcula o hash SHA-512 de uma senha e retorna o hash resultante como uma
    // string hexadecimal. Vou explicar o código em detalhes:

    // public static string Criptografar(string senha) { ... }:

    // Este é o método público que você pode chamar para criptografar uma senha.
    // Ele aceita uma senha como entrada, representada como uma string.
    // var senhaComChaveAdicional = $"{senha}";:

    // Este trecho de código cria uma nova string chamada senhaComChaveAdicional,
    // que é uma cópia da senha original.Não parece adicionar nenhuma "chave adicional"
    // real neste caso, apenas concatena a senha original com uma string vazia.
    // var bytes = Encoding.UTF8.GetBytes(senhaComChaveAdicional);:

    // Aqui, a string senhaComChaveAdicional é convertida em uma matriz de bytes usando
    // a codificação UTF-8. Isso é necessário porque a maioria das funções de hash opera
    // em dados binários.
    // var sha512 = SHA512.Create();:

    // É criada uma instância do algoritmo de hash SHA-512 usando SHA512.Create().
    // Isso permite que você compute o hash usando o algoritmo SHA-512.
    // byte[] hashBytes = sha512.ComputeHash(bytes);:

    // A função ComputeHash é chamada na instância do SHA-512 para calcular o hash
    // dos bytes da senha.O resultado é uma matriz de bytes que representa o hash.
    // return StringBytes(hashBytes);:

    // O método StringBytes é chamado com a matriz de bytes do hash como argumento.
    // StringBytes é uma função privada que converte os bytes do hash em uma
    // representação hexadecimal(string hexadecimal).
    // O resultado final, que é a representação hexadecimal do hash SHA-512,
    // é retornado como uma string.
    // Em resumo, este código é usado para calcular o hash SHA-512 de uma
    // fornecida como entrada e retornar o hash como uma string hexadecimal.
    // Este tipo de função é comumente usado para armazenar senhas de forma
    // segura em um banco de dados, permitindo que você compare hashes em vez
    // de senhas em texto simples durante a autenticação.
