
int numeroMinimo = 1;
int numeroMaximo = 99;
int numeroLinhas = 5;
int numeroColunas = 5;
int pontosLinha = 1;
int pontosColuna = 1;
int pontosCartela = 5;

void Bingo()
{
    // Obter a quantidade de jogadores
    Console.WriteLine("Digite o número de jogadores:");
    int numeroJogadores = int.Parse(Console.ReadLine());

    int[][][] cartelas = new int[numeroJogadores][][];
    int[] numerosSorteados = new int[numeroMaximo];
    int indiceNumeroSorteado = 0;
    bool aVencedor = false;
    int[] pontuacaoJogadores = new int[numeroJogadores];

    // Criar cartelas para cada jogador
    CriarCartelas(cartelas, numeroJogadores);

    // Jogar até que alguém complete a cartela
    while (!aVencedor) // Entra em um loop while que se repete até que um vencedor seja encontrado (aVencedor seja true)
    {
        // Sortear um novo número
        int novoNumero = SortearNumero(numerosSorteados, indiceNumeroSorteado);
        numerosSorteados[indiceNumeroSorteado++] = novoNumero;

        // Mostrar o número sorteado
        Console.WriteLine("\nNúmero sorteado: {0}", novoNumero);

        // Marcar o número nas cartelas dos jogadores
        MarcarNumeroCartela(cartelas, novoNumero, numeroJogadores);

        // Verificar se alguma linha ou coluna foi completada
        VerificarLinhaColunaCompleta(cartelas, numeroJogadores, pontuacaoJogadores);

        // Verificar se há vencedor
        aVencedor = VerificarSeHaVencedor(cartelas, numeroJogadores);

        // Mostrar as cartelas atualizadas
        ExibirCartelas(cartelas, numeroJogadores);
    }

    // Declarar os vencedores
    DeclararVencedores(numeroJogadores, pontuacaoJogadores);
}

// Criar cartelas para cada jogador
void CriarCartelas(int[][][] cartelas, int numeroJogadores)
{
    //  Gera as cartelas de bingo para cada jogador.
    //  Utiliza um loop for para percorrer cada jogador, linha e coluna.
    //  Gera um número aleatório entre numeroMinimo e numeroMaximo usando a Random.
    //  Verifica se o número aleatório já está presente na cartela antes de adicioná - lo.

    Random roleta = new Random();
    for (int jogador = 0; jogador < numeroJogadores; jogador++)
    {
        cartelas[jogador] = new int[numeroLinhas][];

        for (int linha = 0; linha < numeroLinhas; linha++)
        {
            cartelas[jogador][linha] = new int[numeroColunas];

            for (int coluna = 0; coluna < numeroColunas; coluna++)
            {
                int numeroAleatorio;
                do
                {
                    numeroAleatorio = roleta.Next(1, 100); // Valores alterados para evitar constantes
                } while (cartelas[jogador][linha][coluna] != 0 && cartelas[jogador][linha][coluna] != numeroAleatorio);

                cartelas[jogador][linha][coluna] = numeroAleatorio;
            }
        }
    }
}

// Sortear um novo número sem repetição
int SortearNumero(int[] numerosSorteados, int indiceNumeroSorteado)
{
    // Gera um novo número aleatório e sem repetição
    Random roleta2 = new Random();
    int novoNumero;

    do
    {
        novoNumero = roleta2.Next(1, 100); // Valores alterados para evitar constantes
    } while (numerosSorteados[indiceNumeroSorteado - 1] == novoNumero);

    return novoNumero;
}

// Marcar um número na cartela de um jogador
void MarcarNumeroCartela(int[][][] cartelas, int numero, int numeroJogadores)
{
    // Marca o número sorteado nas cartelas de todos os jogadores.
   //   Utiliza loops for para percorrer cada jogador, linha e coluna.
   //  Se o número sorteado for encontrado na cartela, ele é substituído por - 1 para indicar que está marcado.
        for (int jogador = 0; jogador < numeroJogadores; jogador++)
    {
        for (int linha = 0; linha < numeroLinhas; linha++)
        {
            for (int coluna = 0; coluna < numeroColunas; coluna++)
            {
                if (cartelas[jogador][linha][coluna] == numero)
                {
                    cartelas[jogador][linha][coluna] = -1; // Marcar usando valor negativo
                    break;
                }
            }
        }
    }
}

void VerificarLinhaColunaCompleta(int[][][] cartelas, int numeroJogadores, int[] pontuacaoJogadores)
{
    bool linhaCompleta, colunaCompleta;
    // Verifica se alguma linha ou coluna foi completada nas cartelas de cada jogador.
    // Verificar linhas completas para cada jogador
    for (int jogador = 0; jogador < numeroJogadores; jogador++)
    {
        for (int linha = 0; linha < numeroLinhas; linha++)
        {
            linhaCompleta = true;

            for (int coluna = 0; coluna < numeroColunas; coluna++)
            {
                // Verifica se todos os números da linha ou coluna estão marcados (-1).
                if (cartelas[jogador][linha][coluna] != -1) // Número não marcado
                {
                    linhaCompleta = false;
                    break;
                }
            }

            if (linhaCompleta)
            {
                // Adicionar pontos por linha completa
                pontuacaoJogadores[jogador] += pontosLinha;

                // Mostrar mensagem de linha completa
                // Se uma linha ou coluna for completa, a pontuação do jogador é atualizada e uma mensagem é exibida na tela.
                Console.WriteLine("\nJogador {0}: Linha {1} completa!", jogador + 1, linha + 1);
            }
        }
    }

    // Verificar colunas completas para cada jogador
    for (int jogador = 0; jogador < numeroJogadores; jogador++)
    {
        for (int coluna = 0; coluna < numeroColunas; coluna++)
        {
            colunaCompleta = true;

            for (int linha = 0; linha < numeroLinhas; linha++)
            {
                if (cartelas[jogador][linha][coluna] != -1) // Número não marcado
                {
                    colunaCompleta = false;
                    break;
                }
            }

            if (colunaCompleta)
            {
                // Adicionar pontos por coluna completa
                pontuacaoJogadores[jogador] += pontosColuna;

                // Mostrar mensagem de coluna completa
                Console.WriteLine("\nJogador {0}: Coluna {1} completa!", jogador + 1, coluna + 1);
            }
        }
    }
}
// Verificar se há vencedor
bool VerificarSeHaVencedor(int[][][] cartelas, int numeroJogadores)
{
    bool temVencedor = false;
    int[] jogadoresVencedores = new int[numeroJogadores]; // Armazena os índices dos jogadores vencedores
    // Verifica se algum jogador completou a cartela inteira.

    // Verificar se alguma cartela está completa
    for (int jogador = 0; jogador < numeroJogadores; jogador++)
    {
        bool cartelaCompleta = true;

        for (int linha = 0; linha < numeroLinhas; linha++)
        {
            for (int coluna = 0; coluna < numeroColunas; coluna++)
            {
                // Se todos os números da cartela estiverem marcados(-1), o jogador é considerado vencedor.
                if (cartelas[jogador][linha][coluna] != -1)
                {
                    cartelaCompleta = false;
                    break;
                }
            }

            if (!cartelaCompleta)
            {
                break;
            }
        }

        if (cartelaCompleta)
        {
            temVencedor = true;
            jogadoresVencedores[jogador] = 1; // Marcar jogador como vencedor
        }
    }

    // Se houver vencedor, exibir mensagem e declarar os vencedores
    if (temVencedor)
    {
        Console.WriteLine("\n*** Temos um vencedor(es)!!! ***");

        for (int jogador = 0; jogador < numeroJogadores; jogador++)
        {
            if (jogadoresVencedores[jogador] == 1)
            {
                Console.WriteLine("Jogador {0} venceu!", jogador + 1);
            }
        }
    }
    else
    {
        Console.WriteLine("\nAinda não há vencedor!");
    }
    //A função retorna
    return temVencedor;
}

void ExibirCartelas(int[][][] cartelas, int numeroJogadores)
{
    // ExibirCartelas formata os números da cartela para melhor visualização.
    // Exibe as cartelas de bingo atualizadas de todos os jogadores.
    for (int jogador = 0; jogador < numeroJogadores; jogador++)
    {
        Console.WriteLine("\n** Cartela do Jogador {0} **", jogador + 1);

        for (int linha = 0; linha < numeroLinhas; linha++)
        {
            for (int coluna = 0; coluna < numeroColunas; coluna++)
            {
                Console.Write("{0,3}", cartelas[jogador][linha][coluna]);
            }

            Console.WriteLine();
        }
    }
}
void DeclararVencedores(int numeroJogadores, int[] pontuacaoJogadores)
{
    // Se outro jogador tiver a mesma pontuação máxima, a variável empate é definida como true.
    bool empate = true; // Verificar se há empate

    // Encontrar o jogador com maior pontuação
    int maiorPontuacao = pontuacaoJogadores[0];
    int[] jogadoresVencedores = new int[numeroJogadores];

    for (int jogador = 1; jogador < numeroJogadores; jogador++)
    {
        if (pontuacaoJogadores[jogador] > maiorPontuacao)
        {
            maiorPontuacao = pontuacaoJogadores[jogador];
            jogadoresVencedores = new int[numeroJogadores];
            jogadoresVencedores[jogador] = 1; // Marcar novo vencedor
            empate = false;
        }
        else if (pontuacaoJogadores[jogador] == maiorPontuacao)
        {
            jogadoresVencedores[jogador] = 1;
            empate = true;
        }
    }
    // Declarar os vencedores
    if (empate)
    {
        Console.WriteLine("\n*** Temos um empate!!! ***");

        for (int jogador = 0; jogador < numeroJogadores; jogador++)
        {
            if (jogadoresVencedores[jogador] == 1)
            {
                Console.WriteLine("Jogador {0} (pontuação: {1})", jogador + 1, pontuacaoJogadores[jogador]);
            }
        }
    }
    else
    {
        Console.WriteLine("\n*** Temos um vencedor!!! ***");

        for (int jogador = 0; jogador < numeroJogadores; jogador++)
        {
            if (jogadoresVencedores[jogador] == 1)
            {
                Console.WriteLine("Jogador {0} (pontuação: {1})", jogador + 1, pontuacaoJogadores[jogador]);
            }
        }
    }
}