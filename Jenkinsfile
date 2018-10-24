pipeline {
  agent any
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo 'Hello world'
          }
        }
        stage('') {
          steps {
            git(url: 'https://github.com/fankersmit/bluetrain', branch: 'master', changelog: true, poll: true, credentialsId: '590655c7bd576b761f2c5e76ba09ac779e8a0034 ')
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