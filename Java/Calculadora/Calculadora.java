public class Calculadora {

	public static void main(String[] args) {

		Boolean print = true;

		try {
			int num1 = Integer.parseInt(args[0]);
			String operacao = args[1];
			int num2 = Integer.parseInt(args[2]);
			double resultado = 0;


			if (operacao.equals("+")){

				resultado = num1 + num2;

			} else if (operacao.equals("-")){

				resultado = num1 - num2;

			} else if (operacao.equals("x")){

				resultado = num1 * num2;

			} else if (operacao.equals("/")){

				if (num2 != 0){

					resultado = (double) num1 / num2;

				} else {

					System.out.println("\nErro: divisao por zero.");
					print = false;

				}
			} else {

				System.out.println("Erro: Operacao invalida. Favor usar +, -, x ou / apenas");
				print = false;

			}
			if (print){
				System.out.println("\nResultado: " + num1 + " " + operacao + " " + num2 + " = " + resultado);
			}

        } catch (Exception e) {

            System.out.println("\nErro: Parametros ou numeros invalidos.");

        }
    }
}