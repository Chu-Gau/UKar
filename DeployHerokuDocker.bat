docker build -t apitest ./APITest
docker build -t ukar ./UKAR
docker tag apitest registry.heroku.com/chugauapitest/web
docker push registry.heroku.com/chugauapitest/web
docker tag ukar registry.heroku.com/ukar/web
docker push registry.heroku.com/ukar/web
heroku container:release web -a chugauapitest
heroku container:release web -a ukar
