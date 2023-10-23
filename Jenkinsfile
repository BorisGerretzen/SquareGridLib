pipeline {
    agent any
    environment {
        NUGET_KEY = credentials('jenkins-nuget-key')
    }
    stages {
        stage('Install nuke') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'ls '
                    sh 'dotnet tool install --global Nuke.GlobalTool | echo "already installed"'
                }
            }
        }
        stage('Pack') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh "export PATH=${PATH}:${HOME}/.dotnet/tools"
                    sh '/var/jenkins_home/.dotnet/tools/nuke pack'
                }
            }
        }
        stage('Push') {
            steps {
                withDotNet(sdk: '7.0') {
                    sh 'nuke push --NugetApiKey "$NUGET_KEY"'
                }
            }
        }
    }
}
