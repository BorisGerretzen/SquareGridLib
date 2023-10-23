pipeline {
    agent any
    stages {
        stage('Install nuke') {
            steps {
                withDotNet {
                    sh 'dotnet tool install --global Nuke.GlobalTool | echo "already installed"'
                }
            }
        }
        stage('Pack') {
            steps {
                withDotNet {
                    sh 'nuke pack'
                }
            }
        }
        stage('Push') {
            steps {
                withDotNet {
                    sh 'nuke push'
                }
            }
        }
    }
}
