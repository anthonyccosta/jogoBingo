

    // Variáveis para o jogo
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
        while (!aVencedor)
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

        // Verificar linhas completas para cada jogador
        for (int jogador = 0; jogador < numeroJogadores; jogador++)
        {
            for (int linha = 0; linha < numeroLinhas; linha++)
            {
                linhaCompleta = true;

                for (int coluna = 0; coluna < numeroColunas; coluna++)
                {
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

        // Verificar se alguma cartela está completa
        for (int jogador = 0; jogador < numeroJogadores; jogador++)
        {
            bool cartelaCompleta = true;

            for (int linha = 0; linha < numeroLinhas; linha++)
            {
                for (int coluna = 0; coluna < numeroColunas; coluna++)
                {
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
        return temVencedor;
    }

    void ExibirCartelas(int[][][] cartelas, int numeroJogadores)
    {
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