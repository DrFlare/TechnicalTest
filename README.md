U sklopu ovog zadatka implementirani su:

- .NET 9 web API aplikacija koja komunicira s lokalno hostanom MS SQL bazom podataka koristeći EF Core
- Angular web aplikacija koja komunicira s gore navedenom .NET aplikacijom i služi kao njen frontend

Nažalost, zbog vanjskih okolnosti, nisam bio u stanju dovršiti planirane značajke, kao što je logging, exception handling, autentikacija, ili dovršavanje same Angular web aplikacije - koja je tek napola dovršena, ali zbog dogovorenog roka predaje, projekt šaljem u stanju u kojem je sad.


## Upute za pokretanje:
Potrebno je imati instaliran .NET 9.0 SDK, Microsoft SQL Server i Node.js verzije 20.19 ili višе

### Backend
Potrebno je ili otvoriti TechnicalTest.sln u kojemu je spemljena launch konfiguracija za projekt Backend,
ili ručno u direktoriju TechnicalTest/Backend pokrenuti naredbe `dotnet build` i `dotnet run`

### Frontend
Potrebno je instalirati potrebne Node.js pakete naredbom `npm install` u direktoriju TechnicalTest/Frontend i zatim pokrenuti projekt naredbom `npm run start`

### Baza podataka
Baza podataka generira se automatski pokretanjem backend projekta ako već nije, ali mora se puniti ručno. 
Moguće je u bazu ubaciti pripremljene dummy podatke - ili pozivom na <backendURL>/loadDummyData, 
ili gumbom u frontend aplikaciji koji se prikaže u slučaju da je baza prazna.

## Funkcionalnosti:
Backend web API ima endpointe:
```
GET /products - vraća llistu dostupnih proizvoda 
POST /products - ubacije novi proizvod u tablicu (iz JSON podataka u tijelu zahtjeva)

POST /cart/add - dodaje proizvod u košaricu lokalnog korisnika (iz JSON podataka u tijelu zahtjeva), vraća ažurirane podatke o košarici
POST /cart/remove - uklanja proizvod iz košarice lokalnog korisnika (iz JSON podataka u tijelu zahtjeva), vraća ažurirane podatke o košarici
GET /cart/getCartData/{id} - dohvaća podatke o košarici po ID-u
GET /cart/createCart - stvara novu košaricu
GET /cart/completePurchase/{id} - prazni košaricu i šalje zahtjev za uklanjanje one količine predmeta prethodno u košarici iz baze podataka
```

Frotend aplikacija ima dva viewa:
Webshop - prikazuje listu trenutno dostupnih proizvoda (nedovršeno)
Cart - prikazuje listu proizvoda u košarici (uopće nije implementirano)
