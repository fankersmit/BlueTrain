pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git(url: 'https://github.com/fankersmit/BlueTrain', branch: 'master', changelog: true, poll: true, credentialsId: '  GNU nano 2.2.6                New Buffer                            Modified    590655c7bd576b761f2c5e76ba09ac779e8a0034')
      }
    }
    stage('Restore') {
      steps {
        sh 'sh dotnet Restore'
      }
    }
    stage('Clean') {
      steps {
        sh 'sh dotnet clean'
      }
    }
    stage('Build') {
      steps {
        sh 'sh dotnet build --configuration Release'
      }
    }
  }
  environment {
    dotnet = '/usr/bin/dotnet'
  }
}