package com.jacobflasko;

/**
 * Hello world!
 *
 */
public class App {
    public static void main(String[] args) throws InterruptedException {
        System.out.println("Hello World!");
        Thread.sleep(10_000);
        System.out.println("Hello World!");

        for (int j = 1; j <= 10; j++) {
            Thread.sleep(1_000);
            System.out.println(j);
        }

    }
}
