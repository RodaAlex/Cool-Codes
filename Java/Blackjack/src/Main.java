import java.util.Scanner;
public class Main {
    public static void main(String[] args) {
        int[] cartas = new int[52];
        int x = 0;
        boolean continuePlay = true;
        Scanner scanner = new Scanner(System.in);

        while (continuePlay) {
            int totalPlayer = 0;
            int totalDealer = 0;
            int dealerIndex = 0;

            // Preenchendo o array com valores de 1 a 13 repetidos para formar 52 cartas
            for (int i = 0; i < 52; i++) {
                cartas[i] = x + 1;
                x++;
                if (x >= 13) {
                    x = 0;
                }
            }

            // Embaralhando o array de cartas
            for (int i = 51; i > 0; i--) {
                int j = (int) (Math.random() * (i + 1));
                int temp = cartas[i];
                cartas[i] = cartas[j];
                cartas[j] = temp;
            }

            for (int i = 0; i < 52; i++) {
                System.out.println("Sua carta: " + cartas[i]);
                totalPlayer += cartas[i];
                System.out.println("Carta da mesa: " + cartas[++i]);
                totalDealer += cartas[i];
                System.out.println("Seu total: " + totalPlayer + " | Total da mesa: " + totalDealer);

                if (totalPlayer > 21) {
                    System.out.println("Você estourou! Total: " + totalPlayer);
                    break;
                } else if (totalDealer > 21) {
                    System.out.println("A mesa estourou! Total: " + totalDealer);
                    break;
                }

                System.out.print("Deseja continuar? (s/n): ");
                String resposta = scanner.nextLine().trim().toLowerCase();

                if (!resposta.equals("s")) {
                    System.out.println("Encerrando a seleção.");
                    dealerIndex = i;
                    break;
                }
            }

            while (totalDealer < totalPlayer && totalDealer < 21 && totalPlayer < 21) {
                System.out.println("A mesa está jogando...");
                dealerIndex++;
                System.out.println("Carta da mesa: " + cartas[dealerIndex]);
                totalDealer += cartas[dealerIndex];
                System.out.println("Total da mesa: " + totalDealer);
            }


            if (totalPlayer > 21) {
                System.out.println("Você perdeu!");
            } else if (totalDealer > 21 || totalPlayer > totalDealer) {
                System.out.println("Você ganhou!");
            } else if (totalPlayer == totalDealer) {
                System.out.println("Empate! Seu total: " + totalPlayer + " | Total da mesa: " + totalDealer);
            } else {
                System.out.println("Você perdeu!");
            }

            System.out.print("Deseja jogar de novo? (s/n): ");
            String resposta = scanner.nextLine().trim().toLowerCase();

            if (!resposta.equals("s")) {
                System.out.println("Encerrando o jogo.");
                continuePlay = false;
            }
        }
        scanner.close();

    }
}