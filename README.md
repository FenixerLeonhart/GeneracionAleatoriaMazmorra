# GeneracionAleatoriaMazmorra
Unity3D - Generacion Mazmorra Aleatoria con tamaño y cantidad de habitaciones configurables

La idea del proyecto es generar un sendero recto por el cual ir pero que su camino sea aleatorio, ideal para poner generar habitaciones con enemigos y que en la ultima,
este el jefe final para derrotarlo

## Explicacion: 
* Tendremos que generar matriz, con alto y ancho configurables
* Podremos elegir cuantas habitaciones generar antes de la habitacion final
* Crear las habitaciones con salidas distintas (un cuadrador con apertura en +Y y en -Y seria una apertura arriba/abajo, y asi sucesivamente)
* Guardaremos todo el laberinto en un objeto padre para poder modificar su tamaño de forma simple con la escalabilidad
* Y por ultimo y no menos importante, guardaremos el camino recorrido del laberinto en una lista para luego volver a recorrerla y poder generar ahi mismo las habitaciones correspondientes

### Nota:
Para generar esa aleatoriedad hace falta generar antes un recuadro con un objeto simple con un cubo, para luego recorrer ese mismo recorrido y saber las aperturas de
donde esta las aperturas de la habitacion anterior y la siguiente
