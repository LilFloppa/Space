# Asteroids

## Description
Small desktop game about destroying asteroids. Gameplay is similar to this: [Attack the Block](https://play.google.com/store/apps/details?id=com.abi.balls.blockshooter&hl=en&gl=US)

## Gameplay
Your main goal is to score required number of points. You can score points by destroying asteroids. When the required number of points is scored you go to the next level. 
Destoyed asteroids can drop boosters that help you to destroy asteroids faster.

## Used technologies
* C#
* XAML
* [Entity Component System](https://en.wikipedia.org/wiki/Entity_component_system)

# Screenshots

## Main Menu
![image](https://user-images.githubusercontent.com/44492725/132982588-72525fe2-83e1-4900-8dc9-2a176f5b3d70.png)

## Asteroids
![image](https://user-images.githubusercontent.com/44492725/132982598-35e8188f-18a9-4485-9324-3833e40eb70c.png)

## Starship with two lasers and Chainsaw Shield Booster
![image](https://user-images.githubusercontent.com/44492725/132983584-789afcce-5056-4434-8644-1f1989c860e1.png)

## Starship with three lasers
![image](https://user-images.githubusercontent.com/44492725/132983601-c5a9f280-8e2d-42fd-a31b-4ac0cf07ad54.png)

## Level completed
![image](https://user-images.githubusercontent.com/44492725/132983611-f8b3be3c-358c-4fb6-852f-1ade28971108.png)

# Boosters
| Booster | Description |
| ------- | ----------- |
|<img src="https://user-images.githubusercontent.com/44492725/132982664-50c94ddf-5452-4cd8-af27-b4b320c4cc68.png" width=80 height=80 align=center> | **Damage Booster** <br> Increases starship damage by 5. |
|<img src="https://user-images.githubusercontent.com/44492725/132982846-8d7628df-aabb-49c2-84bb-6bd2585bac95.png" width=80 height=80 align=center> | **HP Booster** <br> Increases starship Health Points by 5. |
|<img src="https://user-images.githubusercontent.com/44492725/132982913-a2287de9-7fc7-442f-8e94-7ff091d856c3.png" width=80 height=80 align=center> | **Laser Booster** <br> 1. Increases number of lasers of starship by 1.<br>2. Minimum and maximum number of lasers are 1 and 3 accordingly.<br>3. Each collision of starship with asteroid decreases number of lasers by 1 until it reaches minimum. |
|<img src="https://user-images.githubusercontent.com/44492725/132983354-e3151997-4548-49bd-b9a3-616133d813df.png" width=80 height=80 align=center> | **Bomb** <br> 1. When you pick up this booster all visible asteroids destroy.<br>2. Asteroids destoyed with bomb don't drop boosters. |
|<img src="https://user-images.githubusercontent.com/44492725/132983354-e3151997-4548-49bd-b9a3-616133d813df.png" width=80 height=80 align=center> | **Shield Booster** <br>1. Protects starship from any collision with asteroids.<br>2. Duration - 5 seconds. |
|<img src="https://user-images.githubusercontent.com/44492725/132983476-87aca7d7-cb94-4867-9aea-1f3680cf7c55.png" width=80 height=80 align=center> | **Chainsaw Shield Booster**<br> 1. Acts like shield booster but also deal damage to the asteroid when starship collides with it.<br> 2. Duraction - 5 seconds. |









