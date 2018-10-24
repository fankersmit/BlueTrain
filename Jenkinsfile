pipeline {
  agent any
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo 'building Bluetrain...'
          }
        }
        stage('Checkout') {
          steps {
            git(url: 'https://github.com/fankersmit/bluetrain', branch: 'master', changelog: true, poll: true, credentialsId: '590655c7bd576b761f2c5e76ba09ac779e8a0034 ')
          }
        }
        stage('Restore') {
          agent any
          steps {
            sh 'sh dotnet restore '
          }
        }
        stage('Clean') {
          agent any
          steps {
            sh 'sh dotnet clean'
          }
        }
        stage('Build') {
          agent any
          steps {
            sh 'sh dotnet build --configuration Release'
          }
        }
      }
    }
    stage('Test') {
      steps {
        echo 'Testing...'
      }
    }
    stage('Deploy') {
      agent any
      steps {
        echo 'Deploying...'
      }
    }
  }
}