# gg-coffee-grind-project

Couple remarks:
  * Coffee beans have been updated to look actually like coffee beans. This probably makes detecting collisions way harder, resulting in computational overload, but it does look more sensible this way. The model asset I used can be found [here](https://www.turbosquid.com/3d-models/free-max-model-coffee-beans/254707).
  * These beans are also not prefabricated, so I fixed that. That was also not part of my solution, but I figured they had to be easier to manipulate in bulk. The positions of their transforms are not changed.
  * I used a pyramid-shaped mesh that I changed the size of at runtime as a coffee pile solution. I thought about simulating actual sand-like physics, however I didn't deem that necessary in the scope of this case. This is shaped up to be a mobile game, after all, and complete physics simulation requires a lot of computation.
