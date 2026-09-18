# Configuracion de secretos

## Desarrollo local

1. Copiar `.env.example` como `.env`.
2. Verificar que `PIPELINE_PATH` apunte al repositorio local `leads-pipeline`.
3. Cambiar `POSTGRES_PASSWORD` por una contraseña local.
4. Mantener sincronizados `PIPELINE_DATABASE_URL` y `API_CONNECTION_STRING` con esa contraseña.
5. No subir `.env` al repositorio. Ya está excluido por `.gitignore`.
6. Ejecutar:

```bash
docker compose up --build
```

Para ejecutar la API fuera de Docker, configurar `ConnectionStrings__Default` en el entorno del proceso.

## GitHub Actions

En el repositorio de GitHub:

1. Abrir `Settings`.
2. Entrar en `Secrets and variables` > `Actions`.
3. Seleccionar `New repository secret`.
4. Crear los secretos necesarios, por ejemplo:
   - `POSTGRES_PASSWORD`
   - `API_CONNECTION_STRING`
   - `PIPELINE_DATABASE_URL`
5. Usar esos secretos desde un workflow con `${{ secrets.NOMBRE_DEL_SECRETO }}`.

Los secretos de GitHub solo están disponibles para workflows de GitHub Actions. Si la aplicación se publica en otro proveedor, también hay que registrar allí las variables de entorno del servicio.

## Reglas

- Nunca poner contraseñas reales en `appsettings.json`, `docker-compose.yml`, código fuente o archivos `.example`.
- Si una credencial ya fue subida, rotarla aunque el repositorio sea privado.
- No imprimir cadenas de conexión en logs.
- Separar las credenciales por entorno: local, staging y producción.
- Preferir secretos de entorno o un gestor administrado para producción.
