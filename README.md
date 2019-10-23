EJERCICIOS OPCINALES REALIZADOS
-------------------------------
- Retroceso de armas al disparar
- Adición de una escopeta
- Plano de muerte en el escenario
- Enemigo estático con una IA para disparar al jugador. La IA funciona de la siguiente manera:
  - Tiene un buffer de los ultimos N vectores de velocidad del jugador
  - Utiliza la media del buffer para calcular la posicion futura del jugador
  - Dispara para que el proyectil llege a esa posicion a la vez que el jugador
  - El projectil tiene un movimiento rectilineo uniforme.
  - El enemigo tiene un rango de activacion, si el jugador esta fuera de ese rango, no dispara
  El enemigo puede morir, y al recibir daño se muestra salpicando sangre a su alrededor.
- Al disparar a una pared se quedan marcas de las balas (escopeta y metralleta solo)
