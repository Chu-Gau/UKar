docker build -t apitest ./APITest
docker tag apitest registry.heroku.com/chugauapitest/web
docker push registry.heroku.com/chugauapitest/web
heroku container:release web -a chugauapitest
