# HCI Tim 8
## Zadatak: 5.3CW
VOĐENJE EVIDENCIJE O MAPI LOKALA U NEKOM GRADU

Napraviti jednostavnu aplikaciju za vođenje evidencije o geografskoj distribuciji lokala u
nekom gradu. Potrebno je realizovati distribuciju preko mape grada na koju se prevlače i spuštaju
simboli različitih lokala. Mapa je fiksna slika koja se ne skroluje i ne zumira, i neophodno je da je
studenti nađu sami. Svi podaci se čuvaju u fajlu i učitavaju prilikom startovanja aplikacije.

Svaki **lokal** je opisan preko:
* svoje jedinstvene ljudski-čitljive oznake koju unosi korisnik,
* imena 
* opisa
* tipa
* statusa za služenje alkohola
* ikonice
* da li je dostupan za hendikepirane
* da li je u njemu dozvoljeno pušenje
* da li prima rezervacije
* kategorije cena
* kapaciteta
* datuma otvaranja.

**Ikonica** je sličica koja se učitava i koja se koristi da se lokal označi na mapi i može da se i ne postavi i,
ako se ne postavi, onda se podrazumevano uzima ikonica tipa. 


**Status služenja** alkohola je jedna od sledećih vrednosti:
* ne služi
* služi samo do 23:00
* služi i kasno noću.

**Kategorija cena** je jedna od sledećih vrednosti: 
- niske cene
- srednje cene
- visoke cene
- izuzetno visoke cene. 
 
Lokali takođe mogu biti i "tagovani" sa nijednom, jednom, ili više etiketa. 

**Etikete** specificira korisnik i one su opisane:
- svojom jedinstvenom ljudski-čitljivom oznakom koju unosi korisnik
- bojom
- opisom.

**Tip lokala** je opisan preko:
- svoje jedinstvene ljudski-čitljive oznake koju unosi korisnik
- imena
- ikonice
- opisa. 

Ikonica je sličica koja se učitava i koja se koristi da se taj tip lokala označi na mapi.

#### OSNOVNI ZADACI APLIKACIJE

Aplikacija treba da obezbedi:

1. Ažuriranje osnovnih podataka o lokalima, tipovima i etiketama.
2. Prikaz mape i direktnu manipulaciju simbola na mapi na pregledan način.
3. Nameštanje tipa lokalima kao i njihovo "tagovanje" etiketama
4. Tabelarni pregled lokala uz filtriranje i pretragu.
5. Sistem pomoći.

**NAPOMENA:** Zadatak obavezno realizovati direktnom manipulacijom i upotrebom drag&drop
tehnike. Omogućiti korisniku:

* Vizuelni pregled mape.
* Vizuelni pregled rasporeda lokala.
* Prevlačenje predstave lokala sa kontrole koja vizuelizuje skup dostupnih lokala na predstavu
mape. Lokali se ne smeju preklapati.
* Prikaz svih atributa bitnih za jasnu i jedinstvenu identifikaciju lokala prilikom izbora.

**NASA DODATNA FUNKCIONALNOST**

TUTORIAL

Implementirati u okviru aplikacije podsistem koji omogućava upoznavanje sa nekima od
funkcionalnosti aplikacije preko interaktivne demnostracije (eng. tutorial). Priroda ove demonstracije
zavisi od ostalih faktora zadatka, poglavito profila korisnika.

**NAS KORISNIK B**

<table border="1">
    <tr>
        <th>Pol</th>
        <td>Zenski</td>
    </tr>
    <tr>
        <th>Starost</th>
        <td>27 godina</td>
    </tr>    
    <tr>
        <th>Domensko znanje</th>
        <td>Umereno. Korisnik je upoznat sa osnovnim terminima domenske oblasti u kojoj
radi aplikacija.</td>
    </tr>
      <tr>
        <th>Znanje rada na racunaru</th>
        <td>Srednje. Korisnik je upoznat sa radom na računaru.</td>
    </tr>
      <tr>
        <th>Ogranicavajuce osobine</th>
        <td>Iskustvo korisnika u radu sa računarom je ograničeno na Web aplikacije. Stoga,
korisnik očekuje da se ova (desktop) aplikacija u što većoj meri ponaša kao Web
aplikacija. Valja napomenuti, da se pod ovim ne misli da se aplikacija treba
napraviti koristeći Web tehnologije.</td>
    </tr>
</table> 

**SCENARIO KORISCENJA W**

U ovom scenariju, program se koristi za unos izuzetno velike količine podataka. Zbog toga,
brzina, neposrednost pristupa funkcijama unosa, izmene i brisanja, i efikasnost prečica su od
maksimalne važnosti.

**DODATNA FUNKCIONALNOST ZA TIMSKI RAD**

1. Mapa nije više veličine kontrole i može se skrolovati. Zumiranje i dalje nije neophodno.
2. Na ekranu koji prikazuje mapu neophodno je implementirati pretragu i filtriranje simbola.
3. Neophodno je omogućiti da se istovremeno vodi evidencija o četiri mape, gde svaka ima
svoje podatke, a korisnik može lako birati sa kojom radi u datom trenutku. Omogućiti laku
razmenu podataka između mapa.

## IDE
Visual Studio 2015

## Članovi

* Nina Simić SW34/2013
* Aleksandra Milivojević SW66/2013
* Nikola Lukić SW21/2013
