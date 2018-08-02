README Game Dev Camp


Sujet: Octavia
Nom du projet: Be wise
Membre: langea_g, kabro_c, cousin_q, bastia_k




Lien github: https://github.com/Cruaver/Be-wise


Be wise est un jeu interactif développé sous Unity dans l'intérêt d’améliorer le support pédagogique actuel. Nous avons pour objectif de présenter un jeu où l'élève prend part à l'apprentissage des langues avec l’utilisation de l’outil informatique.


il y a  ici deux objectifs claires dans la démarche de Be wise :


* être un outil complémentaire aux cours du professeur :
L’apprentissage passe dans un premier temps par des cours avec différentes notions et nouveaux mots appris par les élèves.
C’est dans un second temps que les élève pourront compléter leurs apprentissage et l’évaluation sur les mots acquis en cours avec Be wise tout en s'amusant.
* Ne pas se perdre dans le jeux :
La plus grande difficulté est de ne pas se perdre dans l’amusement au détriment de l'apprentissage. C’est pour cette raison que Be wise mise sur un univer ludique ou le principe même du jeu est en accord avec l'apprentissage.        


La simplicité du jeu est aussi un atout dans le développement de l’élève par rapport à l’outil informatique. Mais n’oublions  pas que nous nous adressons au (CM1, CM2) cycle de consolidation, c’est pour cette raison que le jeu implique la mémoir par un processus d'association des mots, avec l’aide de l’écrit. 




Principe : 


Vous incarnez un renard et devez vous déplacer vers des pnj ayant des symboles de dialogues au dessus de leurs têtes, cela afin d’ouvrir un panel d’input et enfin de traduire le texte présent. Français vers Anglais ou inversement.
Pour finir l’étape, vous devez avoir validé tous les mots de la scène. 


A vous de jouer avec le meilleur jeu d’apprentissage des langues !




Langages informatiques utilisés?


C# (Support Unity)
Web GL (export)










Que font les scripts?


AI : 


Script utilisé pour faire en sorte que les pnj détectent des obstacles et agissent en conséquences. Le principe de la méthode Raycast est utilisée afin de tracer une ligne invisible générée depuis un pnj vers le prochain collider qu’il rencontra en face de lui.
Une fois qu’il en rencontre un, il va effectuer une rotation entre 90 et 180° afin de créer un nouveau parcours à chaque collider rencontré. 


Afin que le joueur puisse savoir ou il en est dans l’étape actuelle, des bulles ont été placées au dessus de chacun des pnj. Bulles que nous supprimons afin de ne plus laisser le joueur ouvrir le panel une fois qu’il a effectué la traduction.


transform.Find ("Bubble").gameObject.SetActive (false);


Tout cela calculé une fois par frame grâce à la fonction update.


AnsweringQuestions : 


Ce script permet de créer deux tableaux de données l’un contenant les questions et l’autre les réponses. La fonction update permet à partir du moment ou le panel d’input est ouvert, de vérifier que la réponse correspond bien à la traduction de la question.  
Si la réponse est correcte la fonction IsGoodAnswer() augmente le score du joueur sinon on renvoit l’erreur sous forme de croix dans le panel. Le nombre d’érreur est limité, cependant même si le joueur echoue à répondre correctement, il passera tout de même l’obstacle afin de ne pas pénaliser le joueur sur l’instant T.


BulleScript :


Le script permettant de faire pivoter la bulle des pnj de manière à ce qu’elle soit toujours face au joueur.
public Transform LookTarget  prendra en paramètre le joueur.


CameraOnPlayer :


La caméra est placée derrière le joueur afin qu’il est une vue d’ensemble de tout le terrain.


PlayerMove : 


var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;


ces deux variables récupèrent les valeurs les inputs négatif et positif insérés sous Edit->Project Settings->Input afin que le joueur puisse utiliser ou les flèches directionnelles ou les touches z,q,s,d dans notre cas pour se déplacer.


RandomSpawn : 






Speaking : 


if (collider.tag == "panel" && !collider.GetComponent<AI>().speaked) 


Cette partie du projet est très importante car elle permet de déterminer si le joueur entre en collision avec un collider au tag “panel” et s’il n’a pas déjà parlé au pnj.


Si le joueur n’a rien fait de tel, au premier contact avec l’objet, le panel s’ouvrira


question.PauseForAnswer ();


Nous faisons ici appel à la méthode PauseForAnswer () de la classe AnsweringQuestions afin de mettre en pause le jeu afin de ne pas surcharger le jeu.


collider.GetComponent<AI> ().speaked = true;


En passant la variable speaked de la classe AI à true, nous indiquons que le joueur est déjà rentré en contact avec ce pnj et qu’il ne pourras plus lui parler à l’avenir, le panel ne s’ouvrira plus.




AudioScript :


Ce script permet de poursuivre la musique même si on recharge la page grâce à DontDestroyOnLoad(gameObject);


Recap :

Ce script permet de gérer la fin du programme, la page de récapitulation des score du joueur,  il peut dès lors rejouer la partie. Si le programme contient plus d’un certain nombre de mots à deviner, le joueur a alors accès aux boutons précédent et suivant afin de pouvoir naviguer entre les différentes réponses. 
