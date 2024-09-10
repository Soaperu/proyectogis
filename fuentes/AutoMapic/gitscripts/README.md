## ¿Para qué sirve?

* Permite enviar el archivo *.esriAddIn al directorio de producción

## ¿Cómo configurar?
* Abre una consola de git en el directorio base de automapic
* ejecuta el siguiente comando 

  > _**reemplace {locationpath} por el path real donde se ubica Automapic**_
  
  > _**reemplace \ (backslash) por / (slash) en el path**_

        git config --global alias.autprod '!{locationpath}/AutoMapic/gitscripts/aut_production.bat'
        
* Usa el comando git personalizado

        git autprod