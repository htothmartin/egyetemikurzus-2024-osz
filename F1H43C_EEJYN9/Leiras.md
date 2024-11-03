# Torpedós játék

## Készítette
- Muhel Nimród Róbert
- Hajagos-Tóth Martin

## Kellenek ezek a funkciók: (✖️ / ✅)

### Bejelentkezéskor
- Amikor elindul az alkalmazás akkor kérjen egy felhasználónevet, hogy tudjuk a felhasználót azonosítani ✅
- Ha megadja a felhasználó a nevét akkor megkeressük, hogy az elmentett fileok között megkeressük és betöltjük a felhasználó statisztikáit 
- Ha nincs ilyen akkor pedig létrehozzuk azt a filet, amibenb az adatokat tároljuk ✅.

### Bejelentkezés után
- Megjelenik egy menürendszer ahol a következő pontok lesznek: ✅
+ Egy játék elindítása ✅
+ Beállítások a játékhoz és a konzolhoz: nehézség / hajók száma, mérete / színek (preferenciák) ✅
+ Felhasználóváltás (kijelentkezés) ✅
+ Statisztikák az adott felhasználóról
+ Statisztikák az összes felhasználóról
+ Elkezdett játék betöltése (Ha olyan funkciót létrehozunk, hogy játék menete közben elmentjük játék állapotokat)
+ Kilépés ✅

### A játék beállításainál
+ A gépi játékos nehézségének állítása (Random választ mezőt / Heurisztika alapján / Esetleg AI 🤪)
+ A pálya méretének állítása korlátok között
+ A felhasználók által használható hajók állításai (Például: egy típusú hajót kitiltunk az adott játszmából [2x1] [5x1])
+ A kezdő játékos átállítása (Alapból a felhasználó kezdene, de ezt meg lehet cserélni)
+ Maximális körök beállítása (Ezzel meg lehet adni, hogy a két játékos maximum 30x küldhet torpedót, így utánna döntetlen lesz a játék)
+ A pálya láthatóságánal állítása (Itt azt lehet beállítani, hogy a felhasználónak folyamatosan jelezzük az üres vagy már eltalált hajók helyeit, ezt a konzolban piros X-el ha eltalált egy hajót és Kék X-el ha nem talált el semmit, ha átállítjuk ezt a beállítást akkor a következő körben törlődik annak a helynek a tartalma, így nehezítve a játékot).
+ A játékhoz időt rendelhetünk, amely leteltével a felhasználó autómatikusan veszít

### A Konzol beállításainál
+ A felhasználó megváltoztathatja a játékban használt szimbólumokat (Magának és az ellenségnek is)
+ A felhasználó megváltoztathatja a játékban használt színeket (Magának és az ellenségnek is)

### Kilépés
- Ezzel bezájuk az összes eletleg megnyitott filet, (statisztikák, mentések stb...) és bezárjuk a console-t

### Felhasználó váltás
- Ezzel elmentjük az összes filet ami a mostani felhasználóhoz tartozik és utánna a konzolon megjelenik a bejelentkezésért felelős text input és ugyan az fut le, mint ami az első elindításkor.

### Elkezdett játék betöltése
- Ha megvalósítjuk ezt a funkciót akkor hasonló lesz mint a játék elindítása csak már egy megkezdett játék felülettel


### Statisztikák az adott felhasználóhoz
- Megkeressük a kódból az adott felhasználó nevét és megnyitjuk a hozzá kapcsolódó játékokakt 
- Összesítjük a felhasználóhoz tartozó adatokból a statisztikákhoz szükséges dolgokat
- A kiszámolt statisztikákat elmentjük és megjelenítjük a konzolon

### Statisztikák az összes felhasználóhoz
- Megnyitjük az összes olyan filet amiben fontos adat van a statisztikák számára
- Összesítjük a statisztikákhoz szükséges dolgokat és a statisztikákat kiszámoljuk
- A kiszámolt statisztikákat elmentjük és megjelenítjük a konzolon


### Játék indítása
- A felhasználó számára lejátszunk valami animációt amely alatt betöltjük a játékot és létrehozzuk a hozzá tartozó fileokat
- A felhasználó számára megjelenik a két játék mező ahol a saját pályája és az ellenfelé
- Ez után a felhasználó elhelyezheti a pályára a hajóit, miután ezzel végzett az ellenfélnek is elhelyezzük a hajóit és elmentjük a fileokat
- Miután végeztünk a betöltéssel a játékos számára megjelenik az input field ahova a játék alatt interaktálhat.
- Ez után ki tud adni előre megírt parancsokat ahhoz hogy interaktáljon a játékkal:
+ Támadás
+ Segítség
+ Feladás
+ stb...
- A játék addig megy, ameddig vagy valamelyik játékos utolsó hajója is elsüjjed vagy feladja a játékos a játékot

### A játék után
- Ha véget ért a játék akkor megjelenik egy felület amelyiken a felhasználó teljesítményét írjuk ki ez az adott játékhoz tartozó statisztika:
+ Eltalált hajók
+ Elsüllyedt hajók
+ összes kilőtt torpedó
+ stb...
- Itt a felhasználó három dolgot tud csinálni:
+ A statisztikák lementése
+ A játék újra indítása (visszadob a kezdő képernyőre)
+ Kilépés a játékból (ugyan azt csinálja, mint a menüben levő kilépés)