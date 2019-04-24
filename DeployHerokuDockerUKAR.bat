docker build -t ukar ./UKAR
docker tag ukar registry.heroku.com/ukar/web
docker push registry.heroku.com/ukar/web
heroku container:release web -a ukar
