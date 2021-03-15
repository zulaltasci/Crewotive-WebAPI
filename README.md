Readme
======

* * * * *
* * * * *
* * * * *

<br>

# <p align="center">Person İçin</p>


* * * * *
<br>

### Get İşlemleri

* * * * *

* * * * *

**Active olan bütün Person ları get etmek için =\>** https://{hostname}/api/People

**Passive de dahil bütün Person ları get etmek için =\>** https://{hostname}/api/People/all

**İstenen Id ye sahip Person u get etmek için =\>** https://{hostname}/api/People/{id}

* * * * *
> *Get Formatı*
```json
{
        "id": 1,
        "userName": "beerkaya",
        "name": "Berk",
        "surname": "Kaya",
        "role": "Backend",
        "githubLink": "#",
        "linkedInLink": "#",
        "projects": [
            {
                "id": 1,
                "name": "Project1",
                "explanation": "...",
                "githubLink": "#",
                "isActive": true,
                "personIds": "1"
            },
            {
                "id": 2,
                "name": "Project2",
                "explanation": "...",
                "githubLink": "#",
                "isActive": false,
                "personIds": "1-2"
            }
        ]
    }
```
<br>*"projectids" i "1-2" olan bir person un get edildiğindeki çıktısı.*
<br>*Daha önce silinmiş olan bir proje varsa ve kişi o projede yer alıyorsa, ilgili projenin bilgileri de person get edildiğinde çıktıda yer alır. Projenin kalkmış olduğu da belirtilir.*

<br><br>

### Post İşlemi

* * * * *

* * * * *

**Post etmek için =\>** https://{hostname}/api/People

* * * * *
> ***Person Post Formatı***
```json
{
    "username": "beerkaya",
    "password": "123456",
    "name": "Berk",
    "surname": "Kaya",
    "role": "Backend",
    "githublink": "#",
    "linkedinlink": "#",
    "projectids": ""
}
```
<br>*"projectids" eklenecek proje id lerine göre "1-2-3" şeklinde doldurulur. *
<br>*Herhangi bir proje eklenmemişse boş gönderilir.*

<br><br>

### Put İşlemi

* * * * *

* * * * *

**Put etmek için =\>** https://{hostname}/api/People/{id} 

* * * * *
> ***Person Put Formatı***
```json
{
    "id": 1,
    "username": "beerkaya",
    "name": "Berk",
    "surname": "Kaya",
    "role": "Backend",
    "githublink": "#",
    "linkedinlink": "#",
    "projectids": "1-2"
}
```
*Put edilirken değiştirilen veya değiştirilmeyen tüm bilgiler apiye gönderilir.*
<br>*Burada 2 adet proje olduğu varsayılıp bu kişide "projectids" düzenlemesi yapılmıştır.*


<br><br>

### Delete İşlemi

* * * * *

* * * * *

**Delete etmek için =\>** https://{hostname}/api/People/{id} 

* * * * *


<br><br>

# <p align="center">Project İçin</p>

* * * * *


<br>

### Get İşlemleri

* * * * *

* * * * *

**Active olan bütün Project leri get etmek için =\>** https://{hostname}/api/Projects

**Passive de dahil bütün Project leri get etmek için =\>** https://{hostname}/api/Projects/all

**İstenen Id ye sahip Project i get etmek için =\>** https://{hostname}/api/Projects/{id} 

* * * * *
> *Get Formatı*
```json
{
        "id": 1,
        "name": "Project1",
        "explanation": "...",
        "githubLink": "#",
        "people": [
            {
                "id": 1,
                "userName": "beerkaya",
                "password": "******",
                "name": "Berk",
                "surname": "Kaya",
                "role": "Backend",
                "githubLink": "#",
                "linkedInLink": "#",
                "isActive": true,
                "projectIds": "1-2"
            }
        ]
    }
```
<br>*"personids" i "1" olan bir project in get edildiğindeki çıktısı.*

<br><br>

### Post İşlemi

* * * * *

* * * * *

**Post etmek için =\>** https://{hostname}/api/Projects 

* * * * *
> *Project Post Formatı*
```json
{
    "name": "Project1",
    "explanation": "...",
    "githublink": "#",
    "personids": "1-2-3"
}
```
<br>*"personids" eklenecek person id lerine göre "1-2-3" şeklinde doldurulur. *
<br>*Herhangi bir person eklenmemişse boş gönderilir.*
<br>*Burada 3 adet person olduğu varsayılıp eklemesi yapılmıştır.*


<br><br>

### Put İşlemi

* * * * *

* * * * *

**Put etmek için =\>** https://{hostname}/api/Projects/{id} 

* * * * *
> *Project Post Formatı*
```json
{
    "id": 1,
    "name": "Project1",
    "explanation": "...",
    "githublink": "#",
    "personids": "1-3"
}
```
<br>*Put edilirken değiştirilen veya değiştirilmeyen tüm bilgiler apiye gönderilir.*
<br>*Burada put edilirken "2" numaralı person projeden silinmiştir.*

<br><br>

### Delete İşlemi

* * * * *

* * * * *

**Delete etmek için =\>** https://{hostname}/api/Projects/{id} 

* * * * *


<br><br>

# <p align="center">Admin Paneli</p>

* * * * *
<br>

### Login İşlemi

* * * * *

* * * * *

Login için url e kullanıcı adı ve şifre gönderilerek get isteğinde bulunulur.

**Url =\>** https://{hostname}/api/Login 

* * * * *
> *Login Formatı*
```json
{
    "username": "beerkaya",
    "password": "123456"
}
```
<br>*Apiye bu bilgilerle get isteği gönderilir. Kullanıcı adı ve şifre eşleşir ise true, eşleşmez ise false döner.*
<br>*Diğer durumlarda (username null olması, db de olmaması gibi) "BadRequest" döner.*


<br><br>

### Parola Değiştirme İşlemi

* * * * *

* * * * *

Şifre değiştirme işlemi için url e kullanıcı adı, eski şifre ve yeni şifre gönderilerek put isteğinde bulunulur. İşlem başarılı olursa true döner.

**Url =\>** https://{hostname}/api/People/pass/{id}

* * * * *
> *Parola Değiştirme Formatı*
```json
{
    "username": "beerkaya",
    "oldpassword": "123456"
    "newpassword": "12345678"
}
```
<br>*Kullanıcı adı ve parola eşleşmesi başarılı olursa şifre değiştirilir ve true döner.*
<br>*Diğer durumlarda false döner.*
