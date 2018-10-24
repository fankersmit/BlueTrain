pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Hello world'
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