# T14GoTorz
![Build Status](https://github.com/Ren3mh/T14GoTorz/actions/workflows/build-test.yml/badge.svg)

Team 14 GoTorz 3. semesterprojekt

**Funktionaliteter**

Denne webapp er webshop for billige rejsepakker. Kunder kan browse forskellige rejsepakker, filtrere på lokation og destination samt sortere på pris. Medarbejdere kan bruge appen til at oprette nye rejsepakker. Når brugeren får vist en rejsepakke bliver der lavet et api-kald til en vejrservice så man kan se temperaturen for destinationen.

**Opsætning**

For at benytte nyeste kode, skal man benytte Deployment-branch.

Hvis man selv ønsker at køre webappen, er det nødvendigt at opsætte en database (MSSQL) ud fra DDL.sql (evt. med Dummy1.sql), og indsætte passende connectionstring i en appsettings.json, i GotorzApp.
En OpenWeather api-key, vil også være nødvendig i appsettings, hvis CurrentWeather funktionaliten skal være aktiv.

Besøg evt. andersentech.eu

