Monsieur de Latour: Bonjour!

Vous: Bonjour, comment puis-je vous aider?

Monsieur de Latour: J'ai potentiellement perdu une chaussure dans le campement orc ennemi, seriez-vous en capacité de la ramener? ...

-> choice

=== choice ===

* [Accepter] -> mission_accepted
* [Refuser] -> denied
* [VOUS AVEZ PERDU UNE CHAUSSURE CHEZ DES ORCS???] -> orc

=== mission_accepted ===
Monsieur de Latour: Merci beaucoup brave guerrier, je vous dois une fière chandelle!
-> END

=== denied ===
Monsieur de Latour: J'imagine que je devrais faire appel à quelqu'un d'autre...
-> END

=== orc ===
    

Monsieur de Latour: C'eeest... une longue histoire ^^`

-> choice