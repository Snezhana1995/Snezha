# Бой
Имеются две армии(добро и зло).

В каждой армии есть пехота и всадники.

Заполнение армий происходит произвольным образом.

Сначала сражаются всадники(1 на 1), 
перед сражением определяется, кто из воинов наносит первый удар(произвольным образом), 
при нанесении удара противник теряет силу на уровень силы атакующего воина,
если противник остался жив, он наносит ответный удар по аналогичному принципу, противник погибает, 
если сила удара больше оставшейся силы, после завершения сражения 1 на 1, очередь переходит к следующей паре воинов, 
если в какой-то из армий дошла очередь до последнего воина, то в сражение вступает первый воин из этой армии(цикл), 
в раунде побеждает та армия, в которой остались живые воины, 
после всадников в бой вступает пехотная часть армии(второй раунд)(проходит аналогично первому), 
если по окончанию второго раунда в обоих армиях остаются живые воины, то проходит третий раунд, 
третий раунд проходит аналогично предыдущим, но армии сражаются в перемешку(пешие и всадники), 
при определении первого удара всадники имеют приоритет над пешими.

Побеждает та армия, в которой остался хотя бы 1 живой воин

Иерархия: ![Image alt](https://github.com/Snezhana1995/Snezha/blob/master/ierarhia.png)

Армии выглядят следующим образом: ![Image alt](https://github.com/Snezhana1995/Snezha/blob/master/army.png)
