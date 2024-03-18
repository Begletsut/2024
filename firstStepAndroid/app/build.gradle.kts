import java.util.Date

plugins {
    id("com.android.application")
}

android {
    namespace = "com.example.firstapp"
    compileSdk = 34

    defaultConfig {
        applicationId = "com.example.firstapp"
        minSdk = 23
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
    buildFeatures {
        buildConfig = true
    }
    defaultConfig {
        buildConfigField("String", "BUILD_DATE", "\"${Date()}\"")
    }
}

dependencies {

    implementation("androidx.appcompat:appcompat:1.6.1")
    implementation("com.google.android.material:material:1.11.0")
    implementation("androidx.constraintlayout:constraintlayout:2.1.4")
    implementation("androidx.gridlayout:gridlayout:1.0.0")
    implementation("com.google.android.gms:play-services-location:21.1.0")
    implementation(files("libs/gson-2.8.2.jar"))
    implementation("com.github.bumptech.glide:glide:5.0.0-rc01")
    implementation("androidx.emoji:emoji-bundled:1.1.0")
    implementation("androidx.recyclerview:recyclerview:1.3.2")
    testImplementation("junit:junit:4.13.2")
    androidTestImplementation("androidx.test.ext:junit:1.1.5")
    androidTestImplementation("androidx.test.espresso:espresso-core:3.5.1")
}

