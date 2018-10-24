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
        stage('Checkout') {
          steps {
            git(branch: 'master', url: '\'https://github.com/fankersmit/BlueTrain', credentialsId: 'ido\'tnknow')
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